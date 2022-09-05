using Dictionary.Locals;
using System.Globalization;

namespace Dictionary.Services
{
	class LocalService
	{
		public static readonly LocalService Instance = new();
		public Local GetLocal { get; }
		private LocalService()
		{
			// Локализация. Чтобы вкл. русский: "ru-RU"
			GetLocal = CultureInfo.CurrentUICulture.Name == "ru-RU" ?
						   new Ru() : new En();
		}
	}
}
