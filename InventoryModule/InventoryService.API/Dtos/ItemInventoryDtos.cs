namespace InventoryService.API.Dtos
{
    public record ItemInventoryCreateDto(string InventoryId, string ItemId, int Count);
    public record ItemInventoryUpdateDto(string InventoryId, string ItemId, int Count);
}
