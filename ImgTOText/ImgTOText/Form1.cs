using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IronOcr;
using Spire.Pdf;
using Spire.Pdf.Graphics;


namespace ImgTOText
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        string fileName;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileName = dialog.FileName;
                textBox1.Text = fileName;
                 
            }
            try
            {
              pictureBox1.Image = new Bitmap(fileName);
            }
            catch
            {
                MessageBox.Show("Fayl noto'ri tanlangan rasm tipidagi fayl tanlang");
            }

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var Ocr = new IronTesseract();
            using(var input=new OcrInput(fileName))
            {
                input.Deskew();
                var result = Ocr.Read(input);
                richTextBox1.Text = result.Text;
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text!="")
            {
                
                  
                  
                    PdfDocument doc = new PdfDocument();
                    PdfSection section = doc.Sections.Add();
                    PdfPageBase page = section.Pages.Add();
                    PdfFont font = new PdfFont(PdfFontFamily.Helvetica, 11);
                    PdfStringFormat format = new PdfStringFormat();
                    format.LineSpacing = 20f;
                    PdfBrush brush = PdfBrushes.Black;
                    PdfTextWidget textWidget = new PdfTextWidget(richTextBox1.Text, font, brush);
                    float y = 0;
                    PdfTextLayout textLayout = new PdfTextLayout();
                    textLayout.Break = PdfLayoutBreakType.FitPage;
                    textLayout.Layout = PdfLayoutType.Paginate;
                    RectangleF bounds = new RectangleF(new PointF(0, y), page.Canvas.ClientSize);
                    textWidget.StringFormat = format;
                    textWidget.Draw(page, bounds, textLayout);
                    doc.SaveToFile("E:\\" + textBox2.Text + ".pdf", FileFormat.PDF);
                    MessageBox.Show("Amal bajarildi","Bajarildi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    textBox2.Text = null;
                    richTextBox1.Text = null;
                    pictureBox1.Image = null;
                    textBox1.Text = null;
                
            }
           

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
