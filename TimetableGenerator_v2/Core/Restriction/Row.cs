﻿
namespace Generator.Core.Restriction
{
    public class Row
    {
        public Row(string t, string s, string k, string c, int x, int d)
        {
            this.t = t;
            this.s = s;
            this.k = k;
            this.c = c;
            this.x = x;
            this.d = d;
        }

        /*
            R(t, s, k, c, x, d, b)
            t∈T – преподаватель,
            s∈S – предмет,
            k∈K – кабинет, int? 225 510 string 109а ?
            c∈C – класс (группа), string? 5а 5б 5в
            x∈X – позиция расписания (время проведения), 1,2..7..
            d∈D - день недели 1-ПН 2-ВТ ... 7-ВС
        */
        /// <summary>
        /// t - преподаватель
        /// </summary>
        public string t { get; set; }

        /// <summary>
        /// s - предмет
        /// </summary>
        public string s { get; set; }

        /// <summary>
        /// k - кабинет
        /// </summary>
        public string k { get; set; }

        /// <summary>
        /// c - класс
        /// </summary>
        public string c { get; set; }

        /// <summary>
        /// x - позиция расписания
        /// </summary>
        public int x { get; set; }

        /// <summary>
        /// c - день недели
        /// </summary>
        public int d { get; set; }

    }
}
