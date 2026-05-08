using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Storage
{
    public partial class Form1 : Form
    {
        private List<Pantry> pantrylist;
        private TextBox txtName;
        private TextBox txtAmount;
        private TextBox txtUnits;
        private TextBox txtNumber;
        private Button btnAdd;
        private Button btnRemove;
        private Button btnShow;
        private Button btnSave;
        private Button btnLoad;
        private ListBox lstBox;
        private Label lbnName;
        private Label lbnAmount;
        private Label lbnUnit;
        private Label lbnNumber;
        private Font FontType = new Font("Calibri", 14F);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Pantry Storage";
            this.BackColor = Color.CornflowerBlue;
            btnAdd = new Button() { Text = "Add", Location = new Point(500, 100), AutoSize = true};
            btnAdd.Click += new EventHandler(btnAdd_Click);
            btnRemove = new Button() { Text = "Remove", Location = new Point(500, 100), AutoSize = true };
            btnShow = new Button() { Text = "Show", Location = new Point(500, 150 ), AutoSize = true };
            btnSave = new Button() { Text = "Save", Location = new Point(500, 200), AutoSize = true };
            btnLoad = new Button() { Text = "Load", Location = new Point(500, 250), AutoSize = true };
            btnRemove.Click += new EventHandler(btwRemove_Click);
            btnShow.Click += new EventHandler(btnShow_Click);
            btnSave.Click += new EventHandler(btnSave_Click);
            btnLoad.Click += new EventHandler(btnLoad_Click);
            btnAdd.Font = FontType;
            btnRemove.Font = FontType;
            btnShow.Font = FontType;
            btnSave.Font = FontType;
            btnLoad.Font = FontType;
            pantrylist = new List<Pantry>();
            lstBox = new ListBox() { Location = new Point(50, 50) };
            lstBox.Size = new Size(350, 200);
            lstBox.BorderStyle = BorderStyle.FixedSingle;
            lstBox.Font = FontType;
            lbnName = new Label() { Text = "Name", Location = new Point(50, 325) };
            lbnAmount = new Label() { Text = "Amount", Location = new Point(225, 325) };
            lbnUnit = new Label() { Text = "Units", Location = new Point(375, 325) };
            lbnNumber = new Label() { Text = "Number", Location = new Point(525, 325) };

            lbnName.Font = FontType;
            lbnAmount.Font = FontType;
            lbnUnit.Font = FontType;
            lbnNumber.Font = FontType;
            txtName = new TextBox() { Location = new Point(50, 350) };
            txtName.Font = FontType;
            txtName.Size = new Size(125, 50);
            txtAmount = new TextBox() { Location = new Point(225, 350) };
            txtAmount.Font = FontType;

            txtUnits = new TextBox() { Location = new Point(375, 350) };
            txtUnits.Font = FontType;
            txtNumber = new TextBox() { Location = new Point(525, 350) };
            txtNumber.Font = FontType;

            Controls.Add(btnAdd);
            Controls.Add(btnRemove);
            Controls.Add(btnShow);
            Controls.Add(btnSave);
            Controls.Add(btnLoad);
            Controls.Add(lstBox);
            Controls.Add(lbnName);
            Controls.Add(lbnAmount);
            Controls.Add(lbnUnit);
            Controls.Add(lbnNumber);
            Controls.Add(txtName);
            Controls.Add(txtAmount);
            Controls.Add(txtUnits);
            Controls.Add(txtNumber); 
        }
        public void btnAdd_Click(Object sender, EventArgs e)
        {
            Add(this.txtName.Text, int.Parse(txtAmount.Text), this.txtUnits.Text, int.Parse(txtNumber.Text));
            refreshlist();
        }

        public void Add(string name, int amount, string unit, int number)
        {
            Pantry pantry1 = new Pantry(name, amount, unit, number);
            pantrylist.Add(pantry1); 
            txtName.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtUnits.Text = string.Empty;
            txtNumber.Text = string.Empty;

        }

        public void refreshlist()
        {
            lstBox.Items.Clear();
            foreach (var item in pantrylist)
            {
                lstBox.Items.Add(item.ShowItems());
            }
        }

        public void btwRemove_Click(Object sender, EventArgs e)
        {
            MessageBox.Show("Are you sure, You want to delete?");

            if (lstBox.SelectedIndex < pantrylist.Count)
            {
                pantrylist.Remove(pantrylist[lstBox.SelectedIndex]);
            }
            refreshlist();
        }
        public void btnShow_Click(Object sender, EventArgs e)
        {
            if (lstBox.SelectedIndex < pantrylist.Count)
            {
                txtName.Text = pantrylist[lstBox.SelectedIndex].GetName();
                txtUnits.Text = pantrylist[lstBox.SelectedIndex].GetUnit();
                txtAmount.Text = pantrylist[lstBox.SelectedIndex].GetAmount().ToString();
                txtNumber.Text = pantrylist[lstBox.SelectedIndex].GetNumber().ToString();
            }

        }

        public void btnSave_Click(Object sender, EventArgs e) 
        {
            //string jsonString = JsonSerializer.Serialize(pantrylist, new JsonSerializerOptions
            //{
            //    WriteIndented = true // Für lesbare Formatierung 
            //});

            //File.WriteAllText("personen.json", jsonString);

            // d) Schreibe in und lese aus einer Datei 
            using (StreamWriter writer = new StreamWriter("PantryStorage.txt"))
            {
                writer.WriteLine("Name,Amount,Number"); 
                foreach (var item in pantrylist)
                {
                    writer.WriteLine($"'{item.GetName()}','{item.GetAmount()} {item.GetUnit()}', '{item.GetNumber()}'");
                }
            }
        }

        public void btnLoad_Click(Object sender, EventArgs e)
        {
            MessageBox.Show("Are you sure, You want to load?");
        }
    }

    public class Pantry
    {
        private string Name { get; set; }
        private int Amount { get; set; }

        private string Unit { get; set; }
        private int Number { get; set; }

        public Pantry(string name, int amount, string unit, int number)
        {
            Name = name;
            Unit = unit;
            Amount = amount;
            Number = number;
        }
        public string GetName()
        {
            return Name;
        }
        public int GetAmount()
        {
            return Amount;
        }

        public string GetUnit() 
        {
            return Unit;
        }
        public int GetNumber()
        {
            return Number;
        }
        public string ShowItems()
        {
            return $"{Name}, {Amount} {Unit}, {Number}";
        }
    }
}
