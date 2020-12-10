﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using Spire.Pdf;
using Spire.Pdf.Annotations;
using Spire.Pdf.Widget;
using System.IO;
using iText.Html2pdf;
using System.Drawing.Printing;

namespace HTMLTOPDF
{
    /// <summary>
    /// Interaction logic for ShowBill.xaml
    /// </summary>
    public partial class ShowBill : Page
    {
        int billnum;
        string companyname;
        double price;
        string product;
        int quantity;
        string GSTNO;
        double total;
        double tax;
        string address;
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\PREMIUM\\source\\repos\\HTMLTOPDF\\HTMLTOPDF\\SB.mdf;Integrated Security = True";
        string day;
        string month;
        string year;
        string dirPath;
        string mobno;
        DateTime date;
        
        public ShowBill()
        {
            InitializeComponent();

            SortByID.IsEnabled = false;
            DisableComponents();

            ShowDataByID();
        }

        void DisableComponents()
        {
            BillNo.IsEnabled = false;
            Date.IsEnabled = false;
            GST_NO.IsEnabled = false;
            CompanyName.IsEnabled = false;
            Product.IsEnabled = false;
            Address.IsEnabled = false;
            Quantity.IsEnabled = false;
            Price.IsEnabled = false;
            Tax.IsEnabled = false;
            Total.IsEnabled = false;

            Edit.IsEnabled = false;
            Update.IsEnabled = false;
            Print.IsEnabled = false;
        }

        void EnableComponents()
        {
            BillNo.IsEnabled = true;
            GST_NO.IsEnabled = true;
            CompanyName.IsEnabled = true;
            Product.IsEnabled = true;
            Address.IsEnabled = true;
            Quantity.IsEnabled = true;
            Price.IsEnabled = true;

            Update.IsEnabled = true;
        }

        private void Initialize()
        {
            day = date.Day.ToString();
            month = date.Month.ToString();
            year = date.Year.ToString();
            dirPath = "..\\..\\..\\Bill\\" + year + "\\" + month + "\\";
        }

        private void ShowByID_Click(object sender, RoutedEventArgs e)
        {
            billnum = Convert.ToInt32(billNo.Text);
            string queryString = "SELECT * FROM [dbo].[Bill] WHERE Bill_ID=" + billnum +";";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    BillNo.Text = reader["Bill_ID"].ToString();
                    Date.Text = reader["Date"].ToString();
                    GST_NO.Text = reader["GST_NO"].ToString();
                    CompanyName.Text = reader["Company_Name"].ToString();
                    Address.Text = reader["Address"].ToString();
                    Product.Text = reader["Product_Title"].ToString();
                    Price.Text = reader["Price"].ToString();
                    Quantity.Text = reader["Quantity"].ToString();
                    Tax.Text = reader["Tax"].ToString();
                    Total.Text = reader["Total"].ToString();
                    MobNo.Text = reader["MobNo"].ToString();

                    date = (DateTime)reader["Date"];
                }
            }

            Initialize();
            Edit.IsEnabled = true;
            Print.IsEnabled = true;

        }

        private void ShowByName_Click(object sender, RoutedEventArgs e)
        {
            companyname = companyName.Text;
            string companynameupper = companyname.ToUpper();
            string queryString = "SELECT * FROM [dbo].[Bill] WHERE Company_Name='" + companynameupper + "';";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    BillNo.Text = reader["Bill_ID"].ToString();
                    Date.Text = reader["Date"].ToString();
                    GST_NO.Text = reader["GST_NO"].ToString();
                    CompanyName.Text = reader["Company_Name"].ToString();
                    Address.Text = reader["Address"].ToString();
                    Product.Text = reader["Product_Title"].ToString();
                    Price.Text = reader["Price"].ToString();
                    Quantity.Text = reader["Quantity"].ToString();
                    Tax.Text = reader["Tax"].ToString();
                    Total.Text = reader["Total"].ToString();
                    MobNo.Text = reader["MobNo"].ToString();

                    date = (DateTime)reader["Date"];
                }
            }

            Initialize();
            Edit.IsEnabled = true;
            Print.IsEnabled = true;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SortByCompanyName.IsEnabled = false;
            SortByID.IsEnabled = false;
            string startdate = StartDate.Text;
            string enddate = EndDate.Text;
            string startDate = startdate + " " + "00:00:00";
            string endDate = enddate + " " + "00:00:00";
            string queryString = "SELECT Bill_ID,Company_Name,Date,Product_Title,Total FROM [dbo].[Bill] WHERE (Date BETWEEN CONVERT (DATETIME, '" + startDate + "', 102) AND CONVERT (DATETIME, '" + endDate + "', 102)) ORDER BY Date";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.ExecuteNonQuery();

                SqlDataAdapter dataAdap = new SqlDataAdapter(command);
                DataTable dt = new DataTable("Bill");
                dataAdap.Fill(dt);
                ShowDataTable.ItemsSource = dt.DefaultView;
                dataAdap.Update(dt);
                SortByID.IsEnabled = false;
                SortByCompanyName.IsEnabled = false;
            }
        }

        private void SortByCompanyName_Click(object sender, RoutedEventArgs e)
        {
            SortByCompanyName.IsEnabled = false;
            string queryString = "SELECT Bill_ID,Company_Name,Date,Product_Title,Total FROM [dbo].[Bill] ORDER BY Company_Name;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.ExecuteNonQuery();

                SqlDataAdapter dataAdap = new SqlDataAdapter(command);
                DataTable dt = new DataTable("Bill");
                dataAdap.Fill(dt);
                ShowDataTable.ItemsSource = dt.DefaultView;
                dataAdap.Update(dt);
            }
            SortByID.IsEnabled = true;
        }

        private void ShowDataByID()
        {
            string queryString = "SELECT Bill_ID,Company_Name,Date,Product_Title,Total FROM [dbo].[Bill];";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.ExecuteNonQuery();

                SqlDataAdapter dataAdap = new SqlDataAdapter(command);
                DataTable dt = new DataTable("Bill");
                dataAdap.Fill(dt);
                ShowDataTable.ItemsSource = dt.DefaultView;
                dataAdap.Update(dt);
            }
        }

        private void SortByID_Click(object sender, RoutedEventArgs e)
        {
            SortByID.IsEnabled = false;
            ShowDataByID();
            SortByCompanyName.IsEnabled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EnableComponents();
            Edit.IsEnabled = false;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            DisableComponents();
            //For getting value from textbox
            billnum = Convert.ToInt32(BillNo.Text);
            companyname = CompanyName.Text;
            GSTNO = GST_NO.Text;
            address = Address.Text;
            product = Product.Text;
            quantity = Convert.ToInt32(Quantity.Text);
            //Int32.TryParse(Qauntity.Text,out quantity);
            price = Convert.ToDouble(Price.Text);
            mobno = MobNo.Text;

            string companynameupper = companyname.ToUpper();
            string GSTNOUpper = GSTNO.ToUpper();
            //string addressupper = address.ToUpper();
            //string productupper = product.ToUpper();

            double temp = quantity * price;
            tax = temp * 0.18;
            total = temp + tax;
            
            string queryString = "UPDATE [dbo].[Bill] SET Company_Name='" + companynameupper
                                                  + "',MobNo='" + mobno 
                                                  + "',GST_NO='" + GSTNOUpper 
                                                  + "',Address='" + address
                                                  +"',Product_Title='" + product
                                                  +"',Quantity='" + quantity
                                                  +"',Price='" + price 
                                                  +"',Tax='" + tax 
                                                  +"',Total='" + total 
                                                  +"' WHERE Bill_ID=" + billnum + ";";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }

            string baseUri = "C:\\Users\\PREMIUM\\source\\repos\\HTMLTOPDF";
            string input = "..\\..\\..\\invoice.html";
            string output = dirPath + billnum + ".pdf";

            ConverterProperties properties = new ConverterProperties();
            properties.SetBaseUri(baseUri);


            if (!Directory.Exists(dirPath))
            {
                DirectoryInfo di = Directory.CreateDirectory(dirPath);
            }
            CreateHTML();
            HtmlConverter.ConvertToPdf(new FileStream(input, FileMode.Open, FileAccess.ReadWrite), new FileStream(output, FileMode.Create, FileAccess.ReadWrite), properties);
            Update.IsEnabled = false;
            Edit.IsEnabled = true;
            Print.IsEnabled = true;

            ShowDataByID();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile(dirPath + billnum + ".pdf");

            //Use the default printer to print all the pages
            //doc.PrintDocument.Print();
            //Set the printer and select the pages you want to print

            System.Windows.Forms.PrintDialog dialogPrint = new System.Windows.Forms.PrintDialog();

            if (dialogPrint.ShowDialog() == DialogResult.OK)
            {
                //doc.PrinterName = dialogPrint.PrinterSettings.PrinterName;
                PrintDocument printDoc = doc.PrintDocument;
                printDoc.Print();

            }
        }

        private void Price_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(Price.Text) && !String.IsNullOrWhiteSpace(Quantity.Text))
            {
                int q = Convert.ToInt32(Quantity.Text);
                double p = Convert.ToDouble(Price.Text);

                double te = p * q * 0.18;
                double to = p * q + te;

                Tax.Text = te.ToString();
                Total.Text = to.ToString();
            }
        }

        private void Quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(Price.Text) && !String.IsNullOrWhiteSpace(Quantity.Text))
            {
                int q = Convert.ToInt32(Quantity.Text);
                double p = Convert.ToDouble(Price.Text);

                double te = p * q * 0.18;
                double to = p * q + te;

                Tax.Text = te.ToString();
                Total.Text = to.ToString();
            }
        }

        private void CreateHTML()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<!DOCTYPE html><html><head>");
            sb.Append("<title></title><style type=\"text / css\">");
            sb.Append(".border{margin: 10px;border: solid 1px;font-family: \"Century Gothic\";position: relative;}");
            sb.Append(".border::after {content: \"\";background: url(Khushal.png);background-repeat: no-repeat;opacity: 0.2;top: 375px;left: 100px;bottom: 0;right: 0;position: absolute;z-index: -1;}");
            sb.Append("pre{font-family: \"Century Gothic\";}");
            sb.Append("img{margin-top: 5px;margin-left: 78px;margin-right: 82px;}");
            sb.Append("table{margin-left: 10px;margin-right: 10px;margin-bottom: 10px;}");
            sb.Append("table, th, td{border: 1px solid black;border-collapse: collapse;}");
            sb.Append("th, td{padding-top: 2px;padding-bottom: 2px;}</style></head><body>");
            sb.Append("<div class=\"border\"><img src=\"Khushal.png\" width=\"500px\" height=\"110px\"/><table>");
            sb.Append("<col width=\"46\"><col width=\"295\"><col width=\"50\"><col width=\"97\"><col width=\"90\"><col width=\"104\">");
            sb.Append("<tr><td colspan=\"6\" align=\"center\" style=\"padding-top: 5px;padding-bottom: 5px;\">" + companyname + "</td></tr>");
            sb.Append("<tr><td colspan=\"6\" align=\"center\"><b>Quatation</b></td></tr>");
            sb.Append("<tr><td colspan=\"2\">" + companyname + "</td><td colspan=\"1\"></td><td colspan=\"1\">Qaut. No:</td><td colspan=\"2\">" + billnum + "/" + year + "</td></tr>");
            sb.Append("<tr><td rowspan=\"4\" colspan=\"2\">" + address + ".</td><td></td><td>Buyer's GST:</td><td colspan=\"2\">" + GSTNO + "</td></tr>");
            sb.Append("<tr><td></td><td>Mob No:</td><td colspan=\"2\">" + mobno + "</td></tr>");
            sb.Append("<tr><td></td><td>Date:</td><td colspan=\"2\">" + day + "-" + month + "-" + year + "</td></tr>");
            sb.Append("<tr><td></td><td >&nbsp</td><td colspan=\"2\"></td></tr>");
            sb.Append("<tr><th>Sr No.</th><th>Item Name</th><th>Qty.</th><th>Rate</th><th>GST</th><th>Amount</th></tr><tr>");
            sb.Append("<td rowspan=\"2\" style=\"padding-bottom: 200px;\"><pre>" + 1 + "</pre></td>");
            sb.Append("<td rowspan=\"2\" style=\"padding-bottom: 200px;\"><pre>" + product + "</pre></td>");
            sb.Append("<td rowspan=\"2\" style=\"padding-bottom: 200px;\"><pre>" + quantity + "</pre></td>");
            sb.Append("<td rowspan=\"2\" style=\"padding-bottom: 200px;\"><pre>" + price + "</pre></td>");
            sb.Append("<td rowspan=\"2\" style=\"padding-bottom: 200px;\"><pre>" + tax + "</pre></ytd>");
            sb.Append("<td rowspan=\"2\" style=\"padding-bottom: 200px;\"><pre>" + quantity * price + "</pre></td>");
            sb.Append("</tr><tr></tr>");
            sb.Append("<tr><td colspan=\"4\">" + ConvertWholeNumber(total.ToString()) + "</td><td>Total</td><td>" + total + "</td></tr>");
            sb.Append("<tr><td colspan=\"2\" align=\"right\">GST No.:</td><td colspan=\"4\">24ABNPC5018L2ZO</td></tr>");
            sb.Append("<tr><td colspan=\"6\"><b>Terms & Conditions:</b></td></tr>");
            sb.Append("<tr><td colspan=\"6\"><ul><li style=\"height: 0px; line-height:0px;\">50% Advance at a time booking and 50% balance at time of dispatch our factory</li></ul></td></tr>");
            sb.Append("<tr><td colspan=\"6\"><ul><li style=\"height: 0px; line-height:0px;\">Transaction & loading & uploading by client</li></ul></td></tr>");
            sb.Append("<tr><td colspan=\"6\"><ul><li style=\"height:20px; line-height:15px;\">Training & installation at your site by our enginneer and technician (you have to bear to & from tickets, all the meals and lodging of both the persons</li></ul></td></tr>");
            sb.Append("<tr><td colspan=\"6\"><ul><li style=\"height: 0px; line-height:0px;\">Delivery period:15 days after received advance payment with PO</li></ul></td></tr>");
            sb.Append("<tr><td colspan=\"6\"><ul><li style=\"height: 0px; line-height:0px;\">Order will not be cancelled.</li></ul></td></tr>");
            sb.Append("</table></div></body></html>");

            string html = sb.ToString();
            FileStream fs = new FileStream("..//..//..//invoice.html", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(html);
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        private static String ones(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = "";
            switch (_Number)
            {

                case 1:
                    name = "One";
                    break;
                case 2:
                    name = "Two";
                    break;
                case 3:
                    name = "Three";
                    break;
                case 4:
                    name = "Four";
                    break;
                case 5:
                    name = "Five";
                    break;
                case 6:
                    name = "Six";
                    break;
                case 7:
                    name = "Seven";
                    break;
                case 8:
                    name = "Eight";
                    break;
                case 9:
                    name = "Nine";
                    break;
            }
            return name;
        }

        private static String tens(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = null;
            switch (_Number)
            {
                case 10:
                    name = "Ten";
                    break;
                case 11:
                    name = "Eleven";
                    break;
                case 12:
                    name = "Twelve";
                    break;
                case 13:
                    name = "Thirteen";
                    break;
                case 14:
                    name = "Fourteen";
                    break;
                case 15:
                    name = "Fifteen";
                    break;
                case 16:
                    name = "Sixteen";
                    break;
                case 17:
                    name = "Seventeen";
                    break;
                case 18:
                    name = "Eighteen";
                    break;
                case 19:
                    name = "Nineteen";
                    break;
                case 20:
                    name = "Twenty";
                    break;
                case 30:
                    name = "Thirty";
                    break;
                case 40:
                    name = "Fourty";
                    break;
                case 50:
                    name = "Fifty";
                    break;
                case 60:
                    name = "Sixty";
                    break;
                case 70:
                    name = "Seventy";
                    break;
                case 80:
                    name = "Eighty";
                    break;
                case 90:
                    name = "Ninety";
                    break;
                default:
                    if (_Number > 0)
                    {
                        name = tens(Number.Substring(0, 1) + "0") + " " + ones(Number.Substring(1));
                    }
                    break;
            }
            return name;
        }
        
        private static String ConvertWholeNumber(String Number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX    
                bool isDone = false;//test if already translated    
                double dblAmt = (Convert.ToDouble(Number));
                //if ((dblAmt > 0) && number.StartsWith("0"))    
                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric    
                    beginsZero = Number.StartsWith("0");

                    int numDigits = Number.Length;
                    int pos = 0;//store digit grouping    
                    String place = "";//digit grouping name:hundres,thousand,etc...    
                    switch (numDigits)
                    {
                        case 1://ones' range    

                            word = ones(Number);
                            isDone = true;
                            break;
                        case 2://tens' range    
                            word = tens(Number);
                            isDone = true;
                            break;
                        case 3://hundreds' range    
                            pos = (numDigits % 3) + 1;
                            place = " Hundred ";
                            break;
                        case 4://thousands' range    
                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;
                            place = " Thousand ";
                            break;
                        case 7://millions' range    
                        case 8:
                        case 9:
                            pos = (numDigits % 7) + 1;
                            place = " Million ";
                            break;
                        case 10://Billions's range    
                        case 11:
                        case 12:

                            pos = (numDigits % 10) + 1;
                            place = " Billion ";
                            break;
                        //add extra case options for anything above Billion...    
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)    
                        if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                        {
                            try
                            {
                                word = ConvertWholeNumber(Number.Substring(0, pos)) + place + ConvertWholeNumber(Number.Substring(pos));
                            }
                            catch { }
                        }
                        else
                        {
                            word = ConvertWholeNumber(Number.Substring(0, pos)) + ConvertWholeNumber(Number.Substring(pos));
                        }

                        //check for trailing zeros    
                        //if (beginsZero) word = " and " + word.Trim();    
                    }
                    //ignore digit grouping names    
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch { }
            return word.Trim();
        }
    }
}
