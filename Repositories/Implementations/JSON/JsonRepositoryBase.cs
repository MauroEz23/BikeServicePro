using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeServicePro.Helpers;
using BikeServicePro.Repositories.Interfaces;

namespace BikeServicePro.Repositories.Implementations.JSON
{
    /// <summary>
    /// Clase base abstracta para todos los repositorios basados en JSON
    /// Implementa operaciones CRUD genéricas con manejo de concurrencia
    /// </summary>
    /// <typeparam name="T">Tipo de entidad (debe tener Id)</typeparam>
    public abstract class JsonRepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly string _filePath;
        protected List<T> _entities;
        protected readonly object _lockObject = new();

        /// <summary>
        /// Constructor que inicializa el repositorio con la ruta del archivo JSON
        /// </summary>
        protected JsonRepositoryBase(string fileName)
        {
            _filePath = JsonFileHelper.GetDataStoragePath(fileName);
            _entities = LoadDataAsync().GetAwaiter().GetResult() ?? new List<T>();
        }

        /// <summary>
        /// Carga los datos desde el archivo JSON de forma asíncrona
        /// </summary>
        protected virtual async Task<List<T>> LoadDataAsync()
        {
            return await JsonFileHelper.ReadFromJsonFileAsync<T>(_filePath);
        }

        /// <summary>
        /// Guarda los datos en el archivo JSON de forma asíncrona con locking
        /// </summary>
        protected virtual async Task SaveDataAsync()
        {
            lock (_lockObject)
            {
                JsonFileHelper.WriteToJsonFileAsync(_filePath, _entities).GetAwaiter().GetResult();
            }
        }

        /// <summary>
        /// Obtiene todas las entidades
        /// </summary>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.FromResult(_entities);
        }

        /// <summary>
        /// Obtiene una entidad por su ID
        /// </summary>
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            var property = typeof(T).GetProperty("Id");
            if (property == null)
                throw new InvalidOperationException($"La entidad {typeof(T).Name} no tiene propiedad Id");

            return await Task.FromResult(_entities.FirstOrDefault(e => 
            {
                var value = property.GetValue(e);
                return value != null && (int)value == id;
            }));
        }

        /// <summary>
        /// Agrega una nueva entidad
        /// </summary>
        public virtual async Task<T> AddAsync(T entity)
        {
            var property = typeof(T).GetProperty("Id");
            if (property == null)
                throw new InvalidOperationException($"La entidad {typeof(T).Name} no tiene propiedad Id");

            var maxId = _entities.Any() ? _entities.Max(e => (int)property.GetValue(e)!) : 0;
            property.SetValue(entity, maxId + 1);

            _entities.Add(entity);
            await SaveDataAsync();
            return entity;
        }

        /// <summary>
        /// Actualiza una entidad existente
        /// </summary>
        public virtual async Task UpdateAsync(T entity)
        {
            var property = typeof(T).GetProperty("Id");
            if (property == null)
                throw new InvalidOperationException($"La entidad {typeof(T).Name} no tiene propiedad Id");

            var id = (int)property.GetValue(entity)!;
            var index = _entities.FindIndex(e => (int)property.GetValue(e)! == id);

            if (index >= 0)
            {
                _entities[index] = entity;
                await SaveDataAsync();
            }
        }

        /// <summary>
        /// Elimina una entidad por su ID
        /// </summary>
        public virtual async Task DeleteAsync(int id)
        {
            var property = typeof(T).GetProperty("Id");
            if (property == null)
                throw new InvalidOperationException($"La entidad {typeof(T).Name} no tiene propiedad Id");

            var entity = _entities.FirstOrDefault(e => (int)property.GetValue(e)! == id);
            if (entity != null)
            {
                _entities.Remove(entity);
                await SaveDataAsync();
            }
        }

        /// <summary>
        /// Verifica si una entidad existe
        /// </summary>
        public virtual async Task<bool> ExistsAsync(int id)
        {
            var property = typeof(T).GetProperty("Id");
            if (property == null)
                throw new InvalidOperationException($"La entidad {typeof(T).Name} no tiene propiedad Id");

            return await Task.FromResult(_entities.Any(e => (int)property.GetValue(e)! == id));
        }

        /// <summary>
        /// Guarda todos los cambios pendientes
        /// </summary>
        public virtual async Task SaveChangesAsync()
        {
            await SaveDataAsync();
        }

        /// <summary>
        /// Obtiene el próximo ID disponible
        /// </summary>
        public virtual async Task<int> GetNextIdAsync()
        {
            var property = typeof(T).GetProperty("Id");
            if (property == null)
                throw new InvalidOperationException($"La entidad {typeof(T).Name} no tiene propiedad Id");

            var maxId = _entities.Any() ? _entities.Max(e => (int)property.GetValue(e)!) : 0;
            return await Task.FromResult(maxId + 1);
        }

        /// <summary>
        /// Recarga los datos desde el archivo
        /// </summary>
        public virtual async Task ReloadAsync()
        {
            _entities = await LoadDataAsync() ?? new List<T>();
        }
    }
}