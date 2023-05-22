using Generator.Core.Restriction;
using Generator.Singleton;
using Generator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Generator.Model;


namespace Generator.Core
{
    public class Individual : IComparable<Individual>
    {
        public int[] StrikeOrder { get; private set; }
        public int[] ColorizeOrder { get; private set; }
        public int[] Colors { get; private set; }
        public int ColorsCount { get; private set; }
        public int Rating { get; private set; }

        /// <summary>
        /// Конструктор для случайного порядка
        /// </summary>
        public Individual()
        {
            Colors = new int[Data.Instance.N];
            StrikeOrder = new int[Data.Instance.N];
            ColorizeOrder = new int[Data.Instance.N];
            Array.Copy(Data.Instance.Numbers, ColorizeOrder, Data.Instance.N);
            Rand.Shuffle(ColorizeOrder);
            Encode();
        }

        /// <summary>
        /// Конструктор для создания потомка
        /// </summary>
        /// <param name="indA">Первая особь родитель</param>
        /// <param name="indB">Вторая особь родитель</param>
        public Individual(Individual indA, Individual indB)
        {
            Colors = new int[Data.Instance.N];
            StrikeOrder = new int[Data.Instance.N];
            ColorizeOrder = new int[Data.Instance.N];
            int middle = Data.Instance.N / 2;
            int i;
            for (i = 0; i < middle; i++)
                StrikeOrder[i] = indA.StrikeOrder[i];

            for (i = middle; i < Data.Instance.N; i++)
                StrikeOrder[i] = indB.StrikeOrder[i];

            Decode();
            if (ThreadSafeRandom.ThisThreadsRandom.Next(100) > 95)
                Mutate();
        }

        public void BuildAndColorize()
        {
            ColorizeByOrder();
            CalcRating();
        }

        private void CalcRating()
        {
            // Rating += ApplyRestrictions(this.TableToRows());
            Rating += ApplyRestrictions(this.TableToLessonsForClass());
        }

        // public int GetErrors()
        // {
        //     var lessons = this.TableToLessonsForClass();
        //     var total = 0;
        //     foreach (var lesson in lessons)
        //     {
        //         for (int i = 0; i < 7; ++i)
        //         {
        //             if (!CheckValidLessonsForClass(lesson.Value[i]))
        //             {
        //                 total++;
        //             }
        //         }
        //     }
        //
        //     return total;
        // }
        private int ApplyRestrictions(Dictionary<Class, List<List<KeyValuePair<int, Shift>>>> lessons)
        {
            var restrictions = new Dictionary<Class, List<RestrictionModel>>();
            foreach (var cClass in Data.Instance.Classes.Values)
            {
                restrictions.Add(cClass, new List<RestrictionModel>());
                for (int i = 0; i < 7; ++i)
                    restrictions[cClass].Add(new RestrictionModel(2, 10, 20, false));
            }

            foreach (var restrictionBuilderModel in Data.Instance.RestrictionBuilderModels)
            {
                foreach (var dayOfWeekModel in restrictionBuilderModel.DaysOfWeek)
                {
                    restrictions[restrictionBuilderModel.Class][dayOfWeekModel.Index] = new RestrictionModel(
                        restrictionBuilderModel.Count,
                        restrictionBuilderModel.WeightPositive,
                        restrictionBuilderModel.WeightNegative,
                        true);
                }
            }
            
            var total = 0;
            foreach (var lesson in lessons)
            {
                for (int i = 0; i < 7; ++i)
                {
                    var restrictionBuilderModel = restrictions[lesson.Key][i];
                    if (CheckValidLessonsForClass(lesson.Value[i], restrictionBuilderModel.Count, restrictionBuilderModel.IsImportant))
                    {
                        total += restrictionBuilderModel.WeightPositive;
                    }
                    else
                    {
                        total -= restrictionBuilderModel.WeightNegative;
                    }
                }
            }
            
            return total;
        }


        private bool CheckValidLessonsForClass(List<KeyValuePair<int, Shift>> lessons, int count, bool isImportant)
        {
            if (!isImportant && lessons.Count < 2) return true;
            if (lessons.Count != count) return false;
            if (lessons.Select(x => x.Value).Distinct().Count() != 1) return false;
            var sortedLessons = lessons.OrderBy(x => x.Key).ToList();
            
            for (int i = 1; i < sortedLessons.Count; ++i)
            {
                if (sortedLessons[i].Key - sortedLessons[i - 1].Key != 1) return false;
            }
            
            return true;
        }

        private int ApplyRestrictions(List<Row> rows)
        {
            var restrictions = Data.Instance.Restrictions;
            var total = 0;
            foreach (var restriction in restrictions)
            {
                var result = Compilier.RunMethod(rows, restriction.Method).Split().Select(x => int.Parse(x)).ToList();
                if (restriction.IsRequirement && result[1] > 0)
                {
                    total -= 1000;
                }
                total += result[2];
            }
            return total;
        }

        private void ColorizeByOrder()
        {
            ColorsCount = 0;
            for (int i = 0; i < Data.Instance.N; i++)
            {
                Colors[ColorizeOrder[i]] = 1;
                HashSet<int> usedColors = new HashSet<int>();
                for (int j = 0; j < i; j++)
                {
                    if (i != j && IsAdjacent(i, j))
                    {
                        usedColors.Add(Colors[ColorizeOrder[j]]);
                        while (usedColors.Contains(Colors[ColorizeOrder[i]]))
                            Colors[ColorizeOrder[i]]++;
                        if (ColorsCount < Colors[ColorizeOrder[i]])
                            ColorsCount = Colors[ColorizeOrder[i]];
                    }
                }
            }
        }

        private bool IsAdjacent(int i, int j)
        {
            return Data.Instance.Mas[Data.Instance.Lessons[ColorizeOrder[i]].Id, Data.Instance.Lessons[ColorizeOrder[j]].Id] 
                   && Data.Instance.Lessons[ColorizeOrder[i]].Shift == Data.Instance.Lessons[ColorizeOrder[j]].Shift;
        }

        private void Encode() // закодировать - приведение к виду, удобному для скрещивания
        {
            List<int> numbers = new List<int>(Data.Instance.Numbers);
            int curDelete;
            for (int i = 0; i < Data.Instance.N; i++)
            {
                curDelete = numbers.FindIndex(x => x == ColorizeOrder[i]);
                numbers.RemoveAt(curDelete);
                StrikeOrder[i] = curDelete;
            }
        }

        private void Decode() // раскодировать - преведение к привычному виду порядка раскраски
        {
            List<int> numbers = new List<int>(Data.Instance.Numbers);
            for (int i = 0; i < Data.Instance.N; i++)
            {
                ColorizeOrder[i] = numbers[StrikeOrder[i]];
                numbers.RemoveAt(StrikeOrder[i]);
            }
        }

        /// <summary>
        /// Мутация
        /// </summary>
        /// <param name="count"> количество мутирующих генов </param>
        private void Mutate(int count = 2)
        {
            int firstIndex, secondIndex, tmpValue;
            for (int i = 0; i < count; i++)
            {
                firstIndex = ThreadSafeRandom.ThisThreadsRandom.Next(Data.Instance.N);
                secondIndex = ThreadSafeRandom.ThisThreadsRandom.Next(Data.Instance.N);
                tmpValue = ColorizeOrder[firstIndex];
                ColorizeOrder[firstIndex] = ColorizeOrder[secondIndex];
                ColorizeOrder[secondIndex] = tmpValue;
            }
            Encode();
        }

        int IComparable<Individual>.CompareTo(Individual other)
        {
            return Rating.CompareTo(other.Rating);
        }

    }
}
