using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Student_MVC_Project.Models;

namespace Student_MVC_Project.Service
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public StudentService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            // Use the correct base URL from appsettings
            var apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7258/";
            _httpClient.BaseAddress = new Uri(apiBaseUrl);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            try
            {
                // Note: Direct to microservice uses "api/Student" not "students"
                var response = await _httpClient.GetAsync("api/Student");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Student>>(content);
                }
                return new List<Student>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving students: {ex.Message}");
            }
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Student/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Student>(content);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving student with ID {id}: {ex.Message}");
            }
        }

        public async Task<Student> CreateStudentAsync(Student student)
        {
            try
            {
                var json = JsonConvert.SerializeObject(student);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Use "api/Student" for direct microservice connection
                var response = await _httpClient.PostAsync("api/Student", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Student>(responseContent);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new ApplicationException($"API Error: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error creating student: {ex.Message}");
            }
        }

        public async Task<Student> UpdateStudentAsync(int id, Student student)
        {
            try
            {
                var json = JsonConvert.SerializeObject(student);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/Student/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return student;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new ApplicationException($"API Error: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error updating student: {ex.Message}");
            }
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Student/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error deleting student: {ex.Message}");
            }
        }
    }
}