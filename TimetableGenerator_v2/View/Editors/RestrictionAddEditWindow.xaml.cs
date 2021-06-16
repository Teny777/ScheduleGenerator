using Generator.Core.Restriction;
using Generator.Tools;
using System;
using System.CodeDom.Compiler;
using System.Windows;
using System.Windows.Input;

namespace Generator.View.Editors
{
    /// <summary>
    /// Логика взаимодействия для RestrictionWindow.xaml
    /// </summary>
    public partial class RestrictionAddEditWindow : Window
    {
        public RestrictionAddEditWindow(EditorType editorType, Restriction old = null)
        {
            InitializeComponent();
            DataContext = this;
            CancelCommand = new RelayCommand(Cancel);
            AccessCommand = new RelayCommand(Access);

            switch (editorType)
            {
                case EditorType.Add:
                    Title = "Добавить ограничение";
                    Expression = string.Empty;
                    Comment = string.Empty;
                    WeightPozitive = 0;
                    WeightNegative = 0;
                    break;
                case EditorType.Edit:
                    if (old is null) throw new Exception("old was null");
                    Expression = old.Expression;
                    WeightPozitive = old.WeightPozitive;
                    WeightNegative = old.WeightNegative;
                    Comment = old.Comment;
                    IsRequirement = old.IsRequirement;
                    _ok = old.CountOk;
                    _fail = old.CountFail;
                    Title = "Редактировать ограничение";
                    break;
                default: throw new Exception();
            }
        }

        private readonly int _ok;
        private readonly int _fail;

        public ICommand AccessCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public string Expression { get; set; }
        public int WeightPozitive { get; set; }
        public int WeightNegative { get; set; }
        public string Comment { get; set; }

        public bool IsRequirement { get; set; }

        private void Cancel()
        {
            DialogResult = false;
            Close();
        }

        private void Access()
        {
            RestrictionAnalyzer.Analyzer.Analyze(Expression);

            CompilerResults compiler;
            try
            {
                var text = Compilier.CreateFunction($"{WeightPozitive} {Expression}", WeightNegative);
                compiler = Compilier.Compile(new string[1] { text });
            }
            catch
            {
                MessageBox.Show("Некорректные данные (синтаксическая ошибка)");
                return;
            }
            if (compiler is null)
            {
                MessageBox.Show("Некорректные данные (семантическая ошибка)");
                return;
            }

            var method = Compilier.CreateMethod(compiler);

            Tag = new Restriction()
            {
                Comment = Comment,
                Expression = Expression,
                WeightPozitive = WeightPozitive,
                Method = method,
                WeightNegative = WeightNegative,
                IsRequirement = IsRequirement,
                CountOk = _ok,
                CountFail = _fail,
            };
            DialogResult = true;
            Close();
        }
    }
}
