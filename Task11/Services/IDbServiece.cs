using System.Collections.Generic;
using Task11.Models;

namespace Task11.Services
{
    public interface IDbServiece
    {
        public List<Doctor> getDoctors();
        public void putDoctor(Doctor doc);
        public Doctor editDoctor(Doctor doc);
        public Doctor delDoctor(Doctor doc);
    }
}
