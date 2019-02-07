using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AuthApplication.Validators.Password
{
    public class CustomPasswordValidator : IIdentityValidator<string>
    {
        public int RequiredLength { get; set; }
        
        public CustomPasswordValidator(int length)
        {
            this.RequiredLength = length;
        }

        public Task<IdentityResult> ValidateAsync(string item)
        {
            if (string.IsNullOrEmpty(item) || item.Length < RequiredLength)
            {
                return Task.FromResult(IdentityResult.Failed($"Минимальная длина пароля равна {RequiredLength}"));
            }
            string pattern = "^[0-9]+$";

            if (!Regex.IsMatch(item, pattern))
            {
                return Task.FromResult(IdentityResult.Failed("Пароль должен состоять только из цифр"));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}