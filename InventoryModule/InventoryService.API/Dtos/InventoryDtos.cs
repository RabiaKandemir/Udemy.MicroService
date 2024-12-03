namespace InventoryService.API.Dtos
{
    public record InventoryCreateDto(string PlayerId,string Name);
    public record InventoryUpdateDto(string Id, string PlayerId, string Name);
}
