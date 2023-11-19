namespace Bookify.Core.Consts
{
    public class UserErrors
    {
        public const string MaxLength = "Length Cannot Be More Than {1} Characters";
        public const string Duplicated = "{0} With The Same Name Is Already Existed!";
        public const string DuplicatedBook = "Book with the same name is already exsits with the same author";
        public const string NotAlowedExtensions = "Only .png .jpg .jpeg are allowed!";
        public const string MaxImageSize = "Image Size Cannot be more than 2 MB!";
        public const string NotFutureAllowed = "Date Can't Be In The Future!";
    }
}
