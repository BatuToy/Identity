using System.ComponentModel.DataAnnotations;

namespace  IdentityApp.ViewModels;

public class EditViewModel 
{
    public string? Id { get; set; }
    public string? FullName { get; set; } 

    
    [EmailAddress]
    public string? Email { get; set; } 

    [StringLength(8 ,  ErrorMessage = "Şifreniz en az 8 karakter uzunluğunda olmalıdır.")]
    [DataType(DataType.Password)]
    public string? Password { get; set; } 
    

    [DataType(DataType.Password)]
    [Compare("Password" , ErrorMessage = "Parolanız eşleşmiyor.")]
    public string? ConfirmPassword { get; set; } 
    
}