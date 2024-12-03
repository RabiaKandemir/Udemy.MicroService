namespace AccountService.API.Dtos
{
   public record PlayerCreateDto(string FirstName,string LastName,string UserName);
   public record PlayerUpdateDto(string Id,string FirstName,string LastName,string UserName);
}
