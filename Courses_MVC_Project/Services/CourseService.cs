using Courses_MVC_Project.Models;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace Courses_MVC_Project.Services
{
    public class CourseService:ICourseService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly JsonSerializerOptions _jsonOptions;

        public CourseService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Set base address from appsettings.json
            _httpClient.BaseAddress = new Uri(_configuration["ApiSettings:BaseUrl"]);
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Course");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Course>>(content, _jsonOptions) ?? new List<Course>();

                }
                return new List<Course>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting courses: {ex.Message}");
                return new List<Course>();

            }

        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {

            try
            {
                var response = await _httpClient.GetAsync($"api/Course/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<Course>(content, _jsonOptions);

                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting course: {ex.Message}");
                return null;
            }

        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            try
            {
                var json = JsonSerializer.Serialize(course);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Course", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<Course>(responseContent, _jsonOptions);

                }
                return null;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating course: {ex.Message}");
                return null;
            }
        }



        public async Task<bool> UpdateCourseAsync(int id,Course course)
        {
            try
            {
                var json = JsonSerializer.Serialize(course);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/Course/{id}", content);

                return response.IsSuccessStatusCode;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error updating course: {ex.Message}");
                return false;
            }

        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Course/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting course: {ex.Message}");
                return false;
            }

        }


    }
}
