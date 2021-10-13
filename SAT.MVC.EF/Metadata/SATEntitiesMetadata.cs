using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace SAT.MVC.EF
{


    #region Courses

    public class CourseMetadata
    {
        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Course ID")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [StringLength(50, ErrorMessage = "50 character max.")]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Course Description")]
        public string CourseDescription { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Credit Hours")]
        public byte CreditHours { get; set; }

        [StringLength(250, ErrorMessage = "250 character max.")]
        public string Curriculum { get; set; }

        [StringLength(500, ErrorMessage = "500 character max.")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Active Class")]
        public bool IsActive { get; set; }
    }

    [MetadataType(typeof(CourseMetadata))]
    public partial class Cours
    {

    }

    #endregion

    #region Enrollment

    public class EnrollmentMetadata
    {
        [Required(ErrorMessage = "This is a required field.")]
        [Display(Name = "Enrollment ID")]
        public int EnrollmentId { get; set; }

        [Required(ErrorMessage = "This is a required field.")]
        [Display(Name = "Student ID")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "This is a required field.")]
        [Display(Name = "Class Schedule ID")]
        public int ScheduledClassId { get; set; }

        [Required(ErrorMessage = "This is a required field.")]
        [Display(Name = "Enrollment Date")]
        public System.DateTime EnrollmentDate { get; set; }
    }

    [MetadataType(typeof(EnrollmentMetadata))]
    public partial class Enrollment
    {

    }

    #endregion

    #region Scheduled Class

    public class ScheduledClassMetadata
    {
        [Required(ErrorMessage = "This is a required field.")]
        [Display(Name = "Class Schedule ID")]
        public int ScheduledClassId { get; set; }

        [Required(ErrorMessage = "This is a required field.")]
        [Display(Name = "Course ID")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "This is a required field.")]
        [Display(Name = "Start Date")]
        public System.DateTime StartDate { get; set; }

        [Required(ErrorMessage = "This is a required field.")]
        [Display(Name = "End Date")]
        public System.DateTime EndDate { get; set; }

        [Required(ErrorMessage = "This is a required field.")]
        [Display(Name = "Instructor Name")]
        [StringLength(40, ErrorMessage = "40 character max.")]
        public string InstructorName { get; set; }

        [Required(ErrorMessage = "This is a required field.")]
        [Display(Name = "Location")]
        [StringLength(20, ErrorMessage = "20 character max.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "This is a required field.")]
        [Display(Name = "SCSID")]
        public int SCSID { get; set; }
    }

    [MetadataType(typeof(ScheduledClassMetadata))]
    public partial class ScheduledClass
    {

    }

    #endregion

    #region Scheduled Class Status

    public class ScheduledClassStatusMetadata
    {
        [Required(ErrorMessage = "This is a required field.")]
        public int SSCID { get; set; }

        [Required(ErrorMessage = "This is a required field.")]
        [StringLength(50, ErrorMessage = "50 character max.")]
        public string SCSName { get; set; }

    }

    [MetadataType(typeof(ScheduledClassMetadata))]
    public partial class ScheduledClassStatus
    {

    }

    #endregion

    #region Student

    public class StudentMetadata
    {
        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Student ID")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "First Name")]
        [StringLength(20, ErrorMessage = "20 Character max")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Last Name")]
        [StringLength(20, ErrorMessage = "20 Character max")]
        public string LastName { get; set; }

        [StringLength(15, ErrorMessage = "15 Character max")]
        public string Major { get; set; }

        [StringLength(50, ErrorMessage = "50 Character max")]
        public string Address { get; set; }

        [StringLength(25, ErrorMessage = "25 Character max")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "2 Character max")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        [StringLength(10, ErrorMessage = "10 Character max")]
        public string ZipCode { get; set; }

        [StringLength(13, ErrorMessage = "13 Character max")]
        public string Phone { get; set; }

        [StringLength(60, ErrorMessage = "60 Character max")]
        [EmailAddress(ErrorMessage = "Must be a valid email address.")]
        public string Email { get; set; }

        [Display(Name = "Photo")]
        public string PhotoUrl { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        public int SSID { get; set; }

    }

    [MetadataType(typeof(StudentMetadata))]
    public partial class Student
    {

    }

    #endregion

    #region Student Status

    public class StudentStatusMetadata
    {
        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "SSID")]
        public int SSID { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Student Status Name")]
        [StringLength(30, ErrorMessage = "30 character max.")]
        public string SSName { get; set; }

        [Display(Name = "Status Description")]
        [StringLength(250, ErrorMessage = "250 character max.")]
        public string SSDescription { get; set; }

    }

    [MetadataType(typeof(StudentStatusMetadata))]
    public partial class StudentStatus
    {

    }

    #endregion

}
