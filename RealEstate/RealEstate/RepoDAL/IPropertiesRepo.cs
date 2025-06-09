using RealEstate.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.RepoDAL
{
    public interface IPropertiesRepo
    {
        List<Property> GetAllProperties();

        void addproperty(Propertyviews view);

        Property GetPropertyById(int id);

        void updateproperty(Propertyviews id);

        void deleteproperty(int id);


        

        List<Models.Property> GetPaginatedProperties(
   int page, int pageSize, out int totalProperties,
   string keyword = null, string city = null, string propertyType = null, string status = null);









        void booking(PropertyReviewViewModel vs);

        List<Booking> fetchbooking();
        List<Booking> fetcchbookingbyid(int userid);

        void deletebooking(int id);


        Booking getbookingid(int id);

        void updatebooking(Booking id);


        

        //List<Property> GetPaginatedProperties(int page, int pageSize, out int totalProperties);



        Appointment AddAppointment(Appointment ap);
        List<Appointment> apn();

        List<Appointment> GetAppointmentsByBuyerEmail(string buyerEmail);

        List<Property> GetPropertiesBySellerId(int sellerId);

        List<Appointment> GetAllAppointments();

        List<Appointment> GetAppointmentsForSeller(int sellerId);

        Task AddPropertyAsync(Propertyviews model);


    }
}
