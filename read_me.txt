Problems
1. 
App_Start\UnityConfig - �����������(������������� ����������� � ��������)
����� � ������������ �� ���� ����� ����������� ����������
public HomeController(IObjectFactory objectFactory) {} -��� ��.

�� ����� ��� ��������������� ���������� Providers\HelpdeskRoleProvider
� �� ���� ��� �������!!! ��� �� ���� ��� ���������.
�������� ����� Property - �� ����������. ���� ��� "�� ������� �������"


2.
��������� �������� ���� Include ��� � Entity Framework
������ UserRoleRepository - ��� ����� ������� ���� ������������ 
+ ��� ����� Credentional. � �������� ��� �������� ���� ���� - ��� ��� ��������
����� GetSingle, �� ��������� ... ��������, ��� ����� �� �����.
�  ����� �������� ����� ���� � �������.

3. ������� ��� �� ��� ������:
��������: ����� � ������������ ������ ��� ����� ��������� ���������.
ProductGroupServiceImpl
����� ViewAll()
ProductCategory = new ProductCategoryViewModel(_sourceFactory.CreateRepository<ProductCategory, int>().GetSingle(c.ProductCategoryid))
