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

        public async Task<ICollection<Student>> ReadAsync(string lastName)
        {
            var response = await _client.GetAsync($"/api/students?lastName={lastName}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<ICollection<Student>>();
            }
            return null;
        }

        public async Task<int> CreateAsync(Student entity)
        {
            var response = await _client.PostAsJsonAsync($"/api/students", entity);
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<int>();
            }
            return 0;
        }

        public async Task<ICollection<Student>> ReadAsync()
        {
            var response = await _client.GetAsync("/api/students");
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<ICollection<Student>>();
            }
            return null;
        }

        public async Task<Student> ReadAsync(int id)
        {
            var response = await _client.GetAsync($"/api/students/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Student>();
            }
            return null;
        }

        public async Task UpdateAsync(int id, Student entity)
        {
            var response = await _client.PutAsJsonAsync($"/api/students/{id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"/api/students/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
