Problems
1. 
App_Start\UnityConfig - подключение(сопоставление интерфейсов с классами)
Затем в контроллерах во всех через конструктор передается
public HomeController(IObjectFactory objectFactory) {} -ТУТ ОК.

Но место где переопределение провайдера Providers\HelpdeskRoleProvider
я не могу так сделать!!! Тут не знаю как поступить.
Пробовал через Property - не получается. Пока тут "по кривому написал"


2.
Проблемма заменить типа Include как в Entity Framework
Пример UserRoleRepository - мне нужно считать Роль пользователя 
+ его права Credentional. В модельке это свойство типа лист - оно там работает
метод GetSingle, но написанно ... Наверное, так точно не пишут.
И  также записать потом роль с правами.

3. Глянуть или ок так писать:
Например: когда к определенной Группе мне нужно категорию подтянуть.
ProductGroupServiceImpl
метод ViewAll()
ProductCategory = new ProductCategoryViewModel(_sourceFactory.CreateRepository<ProductCategory, int>().GetSingle(c.ProductCategoryid))
