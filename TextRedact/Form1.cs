using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace TextRedact
{
    public partial class Form1 : Form
    {
        string buffer="";
        string filePath = "";
        string Filter = "Текстовые файлы (*.txt)|*.txt | Мощные текстовые файлы (*.rtf)|*.rtf";
        public Form1()
        {
            InitializeComponent();
            Array colors = Enum.GetValues(typeof(KnownColor));
            foreach (KnownColor knowColor in colors)
                comboBox3.Items.Add(Color.FromKnownColor(knowColor));
            comboBox3.SelectedItem = Color.Black;

            foreach (FontFamily family in FontFamily.Families)
                comboBox1.Items.Add(family.Name);
            comboBox1.SelectedItem = "Arial";

            textBox3.Text = richTextBox1.Text.Length.ToString();
            textBox2.Text = "Изменено";

            richTextBox1.Font = new Font((string)comboBox1.SelectedItem, (int)numericUpDown1.Value, richTextBox1.Font.Style);

            ToolStripMenuItem copyMenuItem = new ToolStripMenuItem("Копировать");
            ToolStripMenuItem pasteMenuItem = new ToolStripMenuItem("Вставить");
            contextMenuStrip1.Items.AddRange(new[] { copyMenuItem, pasteMenuItem });
            richTextBox1.ContextMenuStrip = contextMenuStrip1;
            copyMenuItem.Click += copyMenuItem_Click;
            pasteMenuItem.Click += pasteMenuItem_Click;



        }
        void pasteMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = buffer;
        }
       
        void copyMenuItem_Click(object sender, EventArgs e)
        {
           
            buffer = richTextBox1.SelectedText;
        }

        private void настройкаШрифтаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = Filter;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    if (filePath.IndexOf(".txt") == -1)
                        richTextBox1.LoadFile(filePath, RichTextBoxStreamType.RichText);
                    else
                        richTextBox1.Text = File.ReadAllText(filePath);
                }
            }

        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.Filter = Filter;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog.FileName;
                    if (filePath.IndexOf(".txt") == -1)
                        richTextBox1.SaveFile(filePath, RichTextBoxStreamType.RichText);
                    else
                        File.WriteAllText(filePath, richTextBox1.Text);
                   textBox2.Text = "Сохранено";
                }
            }
        }
        

        

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            richTextBox1.Font = fontDialog1.Font;
        }

        private void поЦенруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void поЛевомуБокуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void поПравомуБокуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void помоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string find = textBox1.Text;
            if (richTextBox1.Text.Contains(find))
            {
                int i = 0;
                while (i <= richTextBox1.Text.Length - find.Length)
                {
                    i = richTextBox1.Text.IndexOf(find, i);
                    if (i < 0) break;
                    richTextBox1.SelectionStart = i;
                    richTextBox1.SelectionLength = find.Length;
                    richTextBox1.SelectionColor = Color.Blue;
                    i += find.Length;
                }
            }
            else
            {
                MessageBox.Show("Не найдено ни одного соответствия результатов");
            }
        }

       

       

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PrintDialog printDialog = new PrintDialog())
            {
                PrintDocument docToPrint = new PrintDocument();
                printDialog.AllowSomePages = true;
                printDialog.ShowHelp = true;

                printDialog.Document = docToPrint;

                DialogResult result = printDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    docToPrint.Print();
                }
            }
        }

        private void сохранитьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (filePath == "")
                сохранитьКакToolStripMenuItem_Click(sender, e);
            else
            {
                if (filePath.IndexOf(".txt") == -1)
                    richTextBox1.SaveFile(filePath, RichTextBoxStreamType.RichText);
                else
                    File.WriteAllText(filePath, richTextBox1.Text);
                textBox2.Text = "Сохранено";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

       

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length == 0)
                richTextBox1.SelectAll();

            if (richTextBox1.SelectionFont.Bold)
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style & ~FontStyle.Bold);
            else
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Bold | richTextBox1.SelectionFont.Style);
            richTextBox1.DeselectAll();
            textBox2.Text = "Изменено";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length == 0)
                richTextBox1.SelectAll();

            if (richTextBox1.SelectionFont.Italic)
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style & ~FontStyle.Italic);
            else
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Italic | richTextBox1.SelectionFont.Style);
            richTextBox1.DeselectAll();
            textBox2.Text = "Изменено";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length == 0)
                richTextBox1.SelectAll();

            if (richTextBox1.SelectionFont.Underline)
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style & ~FontStyle.Underline);
            else
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Underline | richTextBox1.SelectionFont.Style);
            richTextBox1.DeselectAll();
            textBox2.Text = "Изменено";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Font = new Font((string)comboBox1.SelectedItem, (int)numericUpDown1.Value, richTextBox1.Font.Style);
            textBox2.Text = "Изменено";
        }

       

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length == 0)
                richTextBox1.SelectAll();

            richTextBox1.SelectionColor = (Color)comboBox3.SelectedItem;
            richTextBox1.DeselectAll();
            textBox2.Text = "Изменено";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length == 0)
                richTextBox1.SelectAll();
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.Name, (int)numericUpDown1.Value, richTextBox1.SelectionFont.Style);
            textBox2.Text = "Изменено";
            richTextBox1.DeselectAll();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = richTextBox1.Text.Length.ToString();
        }
    }
    }


