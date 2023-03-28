using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proiect_Bonus.Repository;
using Proiect_Bonus.Domain;

namespace Proiect_Bonus.Repository
{
    internal abstract class AbstractRepository<ID, E>: Repository<ID,E> where E : Entity<ID>
    {
        public List<E> entities { get; set; }
        public string filename { get; set; }

        public AbstractRepository(string filename)
        {
            this.filename = filename;
            entities = new List<E>();
            LoadData();
        }

        public abstract void LoadData();

        public void WriteToFile()
        {
            string path = Directory.GetCurrentDirectory() + @"\Data\" + filename;
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (E ent in entities)
                    writer.WriteLine(ent.ToString());
            }
        }

        public void AppendToFile(E entity)
        {
            string path = Directory.GetCurrentDirectory() + @"\Data\" + filename;
            using (StreamWriter writer = new StreamWriter(path, true))
                writer.WriteLine(entity.ToString());
        }

        public List<E> findAll()
        {
            return this.entities;
        }

        public E save(E entity)
        {
            this.entities.Add(entity);
            this.AppendToFile(entity);
            return entity;
        }

        public E delete(ID id)
        {
            E juc = findOne(id);
            this.entities.Remove(this.findOne(id));
            this.WriteToFile();
            return juc;
        }

        public E update(E entity, E newEntity)
        {
            this.entities.Remove(entity);
            this.entities.Add(newEntity);
            this.WriteToFile();
            return newEntity;
        }

        public E findOne(ID id)
        {
            return this.entities.Find(x => x.id.Equals(id));
        }
    }
}
