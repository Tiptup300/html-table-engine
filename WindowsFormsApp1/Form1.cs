using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using test_library_table;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Table t = new Table();

            int bidNumber = 7;
            string bidName = "BIG OLD BID";

            t.Columns.Add(new Column("Building Number", StyleProperty.AlignLeft.Width("48pt"), new ColSpanProperty(1)));
            t.Columns.Add(new Column("Building Name", StyleProperty.AlignLeft.Width("108pt"), new ColSpanProperty(1)));
            t.Columns.Add(new Column("Requestor Name", StyleProperty.AlignLeft.Width("108pt"), new ColSpanProperty(1)));
            t.Columns.Add(new Column("Total Quantity", StyleProperty.AlignRight.Width("48pt"), new ColSpanProperty(1)));
            t.Columns.Add(new Column("Total Extended Price", StyleProperty.AlignRight.Width("72pt"), new ColSpanProperty(1)));


            decimal Total_totalQuantity = 0, Total_finalTotal = 0;

            for (int x = 0; x < 10; x++)
            {
                Row r = new Row();

                int buildingNumber = 7;
                string buildingName = "Sam Elliots Building";
                string requestorName = "Frankie ELEM";
                decimal totalQuantity = 4.5M;
                decimal finalTotal = 3700.37234M;


                // Top Row
                r.Cells.Add(new Cell(buildingNumber.ToString("00"), buildingNumber, StyleProperty.AlignLeft.Width("48pt").Bold(),new RowSpanProperty(3)));
                r.Cells.Add(new Cell(buildingName, StyleProperty.AlignLeft.Width("108pt").Bold(), new RowSpanProperty(3)));
                r.Cells.Add(new Cell(requestorName, StyleProperty.AlignLeft.Width("108pt").Bold()));
                r.Cells.Add(new Cell(totalQuantity.ToString("0.0"), totalQuantity, StyleProperty.AlignRight.Width("48pt")));
                r.Cells.Add(new Cell(finalTotal.ToString("$0.00"), finalTotal, StyleProperty.AlignRight.Width("72pt")));

                Total_totalQuantity += totalQuantity;
                Total_finalTotal += finalTotal;

                t.Rows.Add(r);


                decimal subTotal_totalQuantity = 0, subTotal_finalTotal = 0;

                for (int y = 1; y < 3; y++)
                {
                    // Middle Row
                    Row subRow = new Row();
                    subRow.Cells.Add(new Cell(requestorName, StyleProperty.AlignLeft.Width("108pt").Bold()));
                    subRow.Cells.Add(new Cell(totalQuantity.ToString("0.0"), totalQuantity, StyleProperty.AlignRight.Width("48pt")));
                    subRow.Cells.Add(new Cell(finalTotal.ToString("$0.00"), finalTotal, StyleProperty.AlignRight.Width("72pt")));

                    subTotal_totalQuantity += totalQuantity;
                    subTotal_finalTotal += finalTotal;

                    t.Rows.Add(subRow);
                }

                Row subTotalRow = new Row(new ClassProperty("subtotals"));
                subTotalRow.Cells.Add(new Cell($"Total: { buildingNumber.ToString("00") } { buildingName }", StyleProperty.AlignRight.Bold(), new ColSpanProperty(3)));
                subTotalRow.Cells.Add(new Cell(subTotal_totalQuantity.ToString("0.0"), subTotal_totalQuantity, StyleProperty.AlignRight.Bold()));
                subTotalRow.Cells.Add(new Cell(subTotal_finalTotal.ToString("$0.00"), subTotal_finalTotal, StyleProperty.AlignRight.Bold()));

                Total_totalQuantity += subTotal_totalQuantity;
                Total_finalTotal += subTotal_finalTotal;

                t.Rows.Add(subTotalRow);
            }

            // Total Row
            Row totalRow = new Row(new ClassProperty("totals"));

            totalRow.Cells.Add(new Cell($"Total: { bidNumber.ToString("00") } { bidName }", StyleProperty.AlignRight.Bold(), new ColSpanProperty(3)));
            totalRow.Cells.Add(new Cell(Total_totalQuantity.ToString("0.0"), Total_totalQuantity, StyleProperty.AlignRight.Bold()));
            totalRow.Cells.Add(new Cell(Total_finalTotal.ToString("$0.00"), Total_finalTotal, StyleProperty.AlignRight.Bold()));

            t.Rows.Add(totalRow);

            textBox1.Text = t.ToString();
        }
    }
}
