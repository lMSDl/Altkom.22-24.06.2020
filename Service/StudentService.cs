using Models;
using Service.Interfaces;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class StudentService : IStudentService
    {
        private HttpClient _client;

        public StudentService(HttpClient client)
        {
            this._client = client;
        }
        public StudentService()
        {
            this._client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:59465/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ICollection<Student> Read(string lastName)
        {
            var response = _client.GetAsync($"/api/students?lastName={lastName}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<ICollection<Student>>().Result;
            }
            return null;
        }

        public int Create(Student entity)
        {
            var response = _client.PostAsJsonAsync($"/api/students", entity).Result;
            if(response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<int>().Result;
            }
            return 0;
        }

        public ICollection<Student> Read()
        {
            var response = _client.GetAsync("/api/students").Result;
            if(response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<ICollection<Student>>().Result;
            }
            return null;
        }

        public Student Read(int id)
        {
            var response = _client.GetAsync($"/api/students/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Student>().Result;
            }
            return null;
        }

        public void Update(int id, Student entity)
        {
            var response = _client.PutAsJsonAsync($"/api/students/{id}", entity).Result;
            response.EnsureSuccessStatusCode();
        }

        public void Delete(int id)
        {
            var response = _client.DeleteAsync($"/api/students/{id}").Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
