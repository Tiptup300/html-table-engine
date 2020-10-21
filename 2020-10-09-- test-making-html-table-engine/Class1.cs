using System;
using System.Collections.Generic;
using System.Text;

namespace test_library_table
{
    public class Table
    {
        public List<Column> Columns = new List<Column>();
        public List<Row> Rows = new List<Row>();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<html><head><style>* {padding: 0px 26px;border: 1px solid black;}</style></head><body><table>");
            sb.AppendLine("<table>");
            sb.AppendLine($"<thead>");
            sb.AppendLine($"</thead>");
            sb.AppendLine($"<tbody>");

            foreach (Row r in Rows)
                sb.AppendLine(r.ToString());

            sb.AppendLine($"</tbody>");
            sb.AppendLine($"</table>");
            sb.AppendLine("</body></html>");

            return sb.ToString();
        }
    }

    public class Row
    {
        public List<Cell> Cells = new List<Cell>();
        public List<Property> Properties { get; set; } = new List<Property>();

        public Row()
        {

        }

        public Row(params Property[] properties)
        {
            Properties.AddRange(properties);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"<tr>");
            foreach (Cell c in Cells)
                sb.AppendLine(c.ToString());
            sb.AppendLine($"</tr>");

            return sb.ToString();
        }
    }

    public class Column : Cell
    {
        public Column(string text, params Property[] properties) : base(text, text, properties) 
        {
            ElementType = "hr";
        }
    }

    public class Cell
    {
        public string ElementType { get; set; } = "td";
        public string Text { get; set; }
        public object Data { get; set; }
        public Properties Properties { get; set; } = new Properties();

        public Cell(string text, params Property[] properties) : this(text, text, properties) { }

        public Cell(string text, object data, params Property[] properties)
        {
            Text = text;
            Data = data;
            ElementType = "td";
            Properties.AddRange(properties);
        }

        public override string ToString()
        {
            return $"<{ ElementType } { Properties.ToString() } >{ Text }</{ ElementType }>";
        }
    }

    public class Properties : List<Property>
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var p in this)
            {
                sb.Append(p.ToString());
            }

            return sb.ToString();
        }
    }

    public class Property
    {
        public string Identifier { get; set; }
        public string Value { get; set; }

        public Property(string identifier, string value)
        {
            Identifier = identifier;
            Value = value;
        }

        public override string ToString()
        {
            return $"{ Identifier }=\"{ Value }\"";
        }
    }

    public static class StyleProperties
    {
        public static StyleProperty AlignLeft(this StyleProperty sp)
        {
            return sp.Add(StyleProperty.AlignLeft);
        }

        public static StyleProperty AlignCenter(this StyleProperty sp)
        {
            return sp.Add(StyleProperty.AlignCenter);
        }

        public static StyleProperty AlignRight(this StyleProperty sp)
        {
            return sp.Add(StyleProperty.AlignRight);
        }

        public static StyleProperty Width(this StyleProperty sp, string value)
        {
            return sp.Add($"width: { value }");
        }
        public static StyleProperty Bold(this StyleProperty sp)
        {
            return sp.Add(StyleProperty.Bold);
        }

        public static StyleProperty AlignTop(this StyleProperty sp)
        {
            return sp.Add(StyleProperty.AlignTop);
        }

        public static StyleProperty AlignMiddle(this StyleProperty sp)
        {
            return sp.Add(StyleProperty.AlignMiddle);
        }

        public static StyleProperty AlignBottom(this StyleProperty sp)
        {
            return sp.Add(StyleProperty.AlignBottom);
        }
    }

    public class StyleProperty : Property
    {
        public static StyleProperty AlignLeft { get { return new StyleProperty("text-align:left;"); } }
        public static StyleProperty AlignCenter { get { return new StyleProperty("text-align:center;"); } }
        public static StyleProperty AlignRight { get { return new StyleProperty("text-align:right;"); } }
        public static StyleProperty Bold { get { return new StyleProperty("font-weight:bold;"); } }
        public static StyleProperty AlignTop { get { return new StyleProperty("vertical-alignment:top;"); } }
        public static StyleProperty AlignMiddle { get { return new StyleProperty("vertical-alignment:middle;"); } }
        public static StyleProperty AlignBottom { get { return new StyleProperty("vertical-alignment:bottom;"); } }
        public StyleProperty(string styleParameters) : base("style",styleParameters) { }

        public StyleProperty Add(string additionalValue)
        {
            Value += $"{ additionalValue };";
            return this;
        }

        public StyleProperty Add(StyleProperty additionalStyleProperty)
        {
            Value += $"{ additionalStyleProperty.Value };";
            return this;
        }
    }

    public class ColSpanProperty : Property
    {

        public ColSpanProperty(string number) : base("colspan", number) { }
        public ColSpanProperty(int number) : base("colspan", number.ToString()) { }
    }

    public class RowSpanProperty : Property
    {
        public RowSpanProperty(string number) : base("rowspan", number) { }
        public RowSpanProperty(int number) : base("rowspan", number.ToString() ) { }
    }

    public class ClassProperty : Property
    {
        public ClassProperty(string className) : base("class", className) { }
    }
}
