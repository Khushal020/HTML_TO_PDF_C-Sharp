using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using iText.Html2pdf;


namespace HTMLTOPDF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string baseUri = "C:\\Users\\PREMIUM\\source\\repos\\HTMLTOPDF";
            string input = "..\\..\\..\\invoice.html";
            string output = "..\\..\\..\\Sample.pdf";

            ConverterProperties properties = new ConverterProperties();
            properties.SetBaseUri(baseUri);
            HtmlConverter.ConvertToPdf(new FileStream(input,FileMode.Open,FileAccess.ReadWrite), new FileStream(output,FileMode.Create,FileAccess.ReadWrite),properties);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateBill page = new CreateBill();
            //NavigationService.Navigate(root: page);
            this.Content = page;
        }

        private void ShowBill_Click(object sender, RoutedEventArgs e)
        {
            ShowBill page = new ShowBill();
            this.Content = page;
        }
    }
}
