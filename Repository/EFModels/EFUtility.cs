using Repository.EFModels;

namespace Repository.EFModels
{
    public static class EFUtility
    {
        public static PropertyForSaleDTO ToDto(this PropertyForSale entity)
        {
            PropertyForSaleDTO dto = new PropertyForSaleDTO();
            dto.ID = entity.Id;
            dto.Phone = entity.Phone;
            dto.Longitude = entity.Longitude;
            dto.Latitude = entity.Latitude;
            dto.Location = entity.Location;
            dto.Price = entity.Price;
            dto.OwnerEmail = entity.OwnerEmail;
            dto.Title = entity.Title;
            dto.Area = entity.Area;
            dto.Description = entity.Description;

            return dto;
        }
        public static PropertyForSale ToEntity(this PropertyForSaleDTO dto)
        {
            PropertyForSale entity = new PropertyForSale();
            entity.Id = dto.ID;
            entity.Phone = dto.Phone;
            entity.Longitude = dto.Longitude;
            entity.Latitude = dto.Latitude;
            entity.Location = dto.Location;
            entity.Price = dto.Price;
            entity.OwnerEmail = dto.OwnerEmail;
            entity.Title = dto.Title;
            entity.Area = dto.Area;
            entity.Description = dto.Description;

            return entity;
        }
        public static User ToEntity(this UserDTO dto)
        {
            User entity = new User();
            entity.Name = dto.Name;
            entity.Phone = dto.Phone;
            entity.Email = dto.Email;
            entity.Password = dto.Password;

            return entity;
        }
        public static UserDTO ToDTO(this User entity)
        {
            UserDTO dto = new UserDTO();
            dto.Name = entity.Name;
            dto.Phone = entity.Phone;
            dto.Email = entity.Email;
            dto.Password = entity.Password;

            return dto;
        }
        public static UserWithTokenDTO ToTokenUserDTO(this User entity, string token)
        {
            UserWithTokenDTO dto = new UserWithTokenDTO();
            dto.Name = entity.Name;
            dto.Phone = entity.Phone;
            dto.Email = entity.Email;
            dto.Password = entity.Password;
            dto.Token = token;

            return dto;
        }
        public static UserWithTokenDTO ToTokenUserDTO(this UserDTO entity, string token)
        {
            UserWithTokenDTO dto = new UserWithTokenDTO();
            dto.Name = entity.Name;
            dto.Phone = entity.Phone;
            dto.Email = entity.Email;
            dto.Password = entity.Password;
            dto.Token = token;

            return dto;
        }
    }
}
