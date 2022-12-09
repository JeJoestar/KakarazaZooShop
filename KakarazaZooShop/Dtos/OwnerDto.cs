namespace KakarazaZooShop.WebApi.Dtos
{
    public class OwnerDto : CreateOwnerDto
    {
        public int Id { get; set; }

        public List<PatientDto> Animals { get; set; }
    }
}
