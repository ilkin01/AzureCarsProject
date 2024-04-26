using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Models
{
	public class Review:IEntity
	{
		/// <summary>
		/// Bu Id Əsas Açardır
		/// It Is Id Primary Key
		/// </summary>
		public int Id { get; set; }


		/// <summary>
		/// Hansi Posta Yazildigini Gorsedir
		/// It shows what post it was written to
		/// </summary>
		public int PostId { get; set; }


		///// <summary>
		///// Hansi Userin Yazdigini Saxlayacaq Lakin UI-da Hec Bir Sekilde Gorsedilmeyecek. 
		///// Not Null Qoyulmama Sebebi User Siline Biler Lakin Comment Helede Orada Qalmalidir
		///// 
		///// EN - It will save what the user writes, but it will not be displayed in any way in the UI.
		///// The user can delete the reason for not setting null, but the comment must remain there
		///// </summary>
		//public int UserId { get; set; }


		/// <summary>
		/// Review Icerisinde Yazilan Şərhdi
		/// EN - Comment written in Review
		/// </summary>
		public string Comment { get; set; }

		/// <summary>
		/// Bu Ise Reviewin Her Hansisa Mesuliyetli Sexs Terefinden Veya Istifadecinin Ozu Terefinden Silindiyi Haqda Melumatdir
		/// EN - This Ise Review Is Deleted By Any Responsible Person Or By The User Himself
		/// </summary>
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Reviewin Yazildigi tarixi Bildirir
		/// EN - Indicates the date the review was written
		/// </summary>
		public DateTime Date { get; set; }

        public virtual User? User { get; set; }
        public virtual Post Post { get; set; }

    }
}
