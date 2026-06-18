using System.Windows;
using System.Windows.Controls;
using BooksInventoryCodeFirst.Data;
using BooksInventoryCodeFirst.Models;
using BooksInventoryCodeFirst.Services;

namespace BooksInventoryCodeFirst
{
    public partial class MainWindow : Window
    {
        private readonly BookInventoryService _bookInventoryService;

        public MainWindow()
        {
            InitializeComponent();
            _bookInventoryService = new BookInventoryService(new BookInventoryContext());
            LoadBooks();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!TryCreateBookFromForm(out Book? book) || book is null)
            {
                return;
            }

            if (_bookInventoryService.GetBookByIsbn(book.ISBN) is not null)
            {
                SetStatus("A book with this ISBN already exists.");
                return;
            }

            _bookInventoryService.AddBook(book);
            LoadBooks();
            ClearForm();
            SetStatus("Book added.");
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!TryCreateBookFromForm(out Book? book) || book is null)
            {
                return;
            }

            if (_bookInventoryService.GetBookByIsbn(book.ISBN) is null)
            {
                SetStatus("No book was found with that ISBN.");
                return;
            }

            _bookInventoryService.UpdateBook(book);
            LoadBooks();
            SetStatus("Book updated.");
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string isbn = IsbnTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(isbn))
            {
                SetStatus("Enter or select an ISBN before deleting.");
                return;
            }

            _bookInventoryService.DeleteBook(isbn);
            LoadBooks();
            ClearForm();
            SetStatus("Book deleted.");
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            SetStatus("Form cleared.");
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadBooks();
            SetStatus("Book list refreshed.");
        }

        private void BooksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is not Book book)
            {
                return;
            }

            IsbnTextBox.Text = book.ISBN;
            NameTextBox.Text = book.Name;
            AuthorNameTextBox.Text = book.AuthorName;
            DescriptionTextBox.Text = book.Description;
            PublisherTextBox.Text = book.Publisher;
            PriceTextBox.Text = book.Price.ToString("0.00");
            QuantityTextBox.Text = book.QuantityInStock.ToString();
        }

        private void LoadBooks()
        {
            BooksDataGrid.ItemsSource = _bookInventoryService.GetAllBooks();
        }

        private bool TryCreateBookFromForm(out Book? book)
        {
            book = null;

            string isbn = IsbnTextBox.Text.Trim();
            string name = NameTextBox.Text.Trim();
            string authorName = AuthorNameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(isbn) ||
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(authorName))
            {
                SetStatus("ISBN, name, and author name are required.");
                return false;
            }

            if (!decimal.TryParse(PriceTextBox.Text.Trim(), out decimal price) || price < 0)
            {
                SetStatus("Price must be a valid number greater than or equal to zero.");
                return false;
            }

            if (!int.TryParse(QuantityTextBox.Text.Trim(), out int quantity) || quantity < 0)
            {
                SetStatus("Quantity must be a whole number greater than or equal to zero.");
                return false;
            }

            book = new Book
            {
                ISBN = isbn,
                Name = name,
                AuthorName = authorName,
                Description = DescriptionTextBox.Text.Trim(),
                Publisher = PublisherTextBox.Text.Trim(),
                Price = price,
                QuantityInStock = quantity
            };

            return true;
        }

        private void ClearForm()
        {
            BooksDataGrid.SelectedItem = null;
            IsbnTextBox.Clear();
            NameTextBox.Clear();
            AuthorNameTextBox.Clear();
            DescriptionTextBox.Clear();
            PublisherTextBox.Clear();
            PriceTextBox.Clear();
            QuantityTextBox.Clear();
            IsbnTextBox.Focus();
        }

        private void SetStatus(string message)
        {
            StatusTextBlock.Text = message;
        }
    }
}
