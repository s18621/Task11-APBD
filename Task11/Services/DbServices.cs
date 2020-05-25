using System;
using System.Collections.Generic;
using System.Linq;
using Task11.Models;
using Microsoft.EntityFrameworkCore;

namespace Task11.Services
{
    public class DbServices : IDbServiece
    {
        private readonly s18621Context context;
        public DbServices(s18621Context context)
        {
            this.context = context;
        }
        public List<Doctor> getDoctors()
        {
            return context.Doctor.ToList();
        }
        public void putDoctor(Doctor doc)
        {
            context.Doctor.Add(doc);
            context.SaveChanges();
        }     
        public Doctor editDoctor(Doctor doc)
        {
            try
            {
                context.Attach(doc);
                context.Entry(doc).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return doc;
        }
        public Doctor delDoctor(Doctor doc)
        {
            var doct = context.Doctor.FirstOrDefault(s => s.IdDoctor == doc.IdDoctor);
            if (doct == null) return null;
            context.Attach(doct);
            context.Remove(doct);
            context.SaveChanges();
            return doct;
        }
    }
}
