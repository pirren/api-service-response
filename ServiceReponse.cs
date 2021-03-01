using System.Threading.Tasks;

namespace ServiceResponseT
{
    public class EntityModel { public string Data { get; set; } } // domain
    public class EntityModelInfo { public string ParsedData { get; set; } } // dto out
    public static class EntityModelExtensions
    {
        public static EntityModelInfo ToDto(this EntityModel model) => new EntityModelInfo { ParsedData = model.Data }; // map object
    }

    public class SomeService 
    {
        public ServiceResponse<EntityModelInfo> GetPosition(string id)
        {
            EntityModel ent = new EntityModel();
            if (ent == null)
                return new ServiceResponse<EntityModelInfo>($"There is no entity with ID: {id}");

            var dto = ent.ToDto();
            return new ServiceResponse<EntityModelInfo>(dto);
        }
    }

    public class ServiceResponse<T> where T : class
    {
        public T Entity { get; private set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public ServiceResponse(bool success, string message, T entity)
        {
            this.Entity = entity;
            Success = success;
            Message = message;
        }

        /// <summary>
        /// Creates a success response
        /// </summary>
        /// <param name="entity"></param>
        public ServiceResponse(T entity) : this(true, string.Empty, entity)
        { }

        /// <summary>
        /// Creates an error response
        /// </summary>
        /// <param name="message"></param>
        public ServiceResponse(string message) : this(false, message, null)
        { }
    }
}
