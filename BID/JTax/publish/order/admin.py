from django.contrib import admin
from order.models import *


@admin.register(Area)
class AreaAdmin(admin.ModelAdmin):
	list_display=('Code','Name','ParentId')
	list_per_page = 20
	ordering = ('Code',)

@admin.register(Org)
class OrgAdmin(admin.ModelAdmin):
	list_display=('Name','Latitude','Longitude','Type','Representative')
	list_per_page=20
	ordering=('Name',)


@admin.register(OrgCategory)
class OrgCategoryAdmin(admin.ModelAdmin):
	list_display=('Name','AliasName')
	list_per_page = 20
	ordering = ('Name',)
	

@admin.register(PurchaseCategory)
class PurchaseCategoryAdmin(admin.ModelAdmin):
	list_display=('Name','ParentId')
	list_per_page=20
	ordering=('Name',)


@admin.register(OrderDetail)
class OrderDetailAdmin(admin.ModelAdmin):
	list_display=('UserId','ProductName','AmountBeforePayment','Receipts','TotalAmount')
	list_per_page = 20
	ordering = ('UserId',)

@admin.register(BonusPoints)
class BonusPointsAdmin(admin.ModelAdmin):
	list_display=('ProductPrice','RatiofReceiptsAndBonuspoint','RatiofBonuspointReward','RefundDeadline')
	list_per_page=20
	ordering=('ProductPrice',)

@admin.register(OrgUser)
class OrgUserAdmin(admin.ModelAdmin):
	list_display=('UserId','OrgId')
	list_per_page=20
	ordering=('UserId',)


@admin.register(BiddingInfo)
class BiddingInfoAdmin(admin.ModelAdmin):
	list_display=('Url','Title','PurchaseDept','PurchseArea','PublishTime','DeadLine','BiddingSubject','BiddingStatus')
	list_per_page=20
	ordering=('DeadLine','Title','BiddingStatus')


@admin.register(UserProfile) 
class UserProfileAdmin(admin.ModelAdmin):
	list_display=('Phone','WeixinId','Belongto','PromoteCode')
	list_per_page=20
	ordering=('Phone',)


@admin.register(FootPrint)
class FootPrintAdmin(admin.ModelAdmin):
	list_display=('BidInfoId','FavoTag','OrgId','PurchaseCateId','UserId')
	list_per_page = 20
	ordering =('AreaId',)
