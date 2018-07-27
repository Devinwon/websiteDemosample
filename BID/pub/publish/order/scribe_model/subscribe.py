from django.db import models
from django.contrib.auth.models import User
import django.utils.timezone as timezone
from ..models import *


###############################################
'''
用户订阅
'''
class UserSubscribe(models.Model):
	User=models.ForeignKey(to=User, related_name='userid_scribe',null=True )
	ScribeName=models.CharField(max_length=100)
	OrgCategory = models.ForeignKey(to=OrgCategory, related_name='orgcategory_scribe', null=True)  # 采购单位性质
	OrgName = models.CharField(max_length=100)
	SeqNo = models.CharField(max_length=100, null=True)
	PurchseArea = models.ForeignKey(to=Area, related_name='purchsearea_scribe', null=True)  # 采购地区
	AreaName = models.CharField(max_length=100, null=True)
	UpdateTime=models.TimeField(auto_now=True)
	PurchaseCategory = models.ForeignKey(to=PurchaseCategory, related_name='purchasecategory_scribe', null=True)  # 采购目录
	PurchaseName = models.CharField(max_length=100, null=True)
	KeyWord=models.CharField(max_length=500, null=True)

	class Meta:
		verbose_name='UserSubscribe'
		verbose_name_plural = verbose_name

	def __str__(self):
		return self.ScribeName


###############################################
'''
招标信息明细表
'''
class UserBidding(models.Model):

	UserId=models.OneToOneField(to=User, related_name='biddinguserid')
	UserBiddingId = models.ForeignKey(to=BiddingInfo ,related_name='biddinginfo' ,null=True ) # 匹配后的招标信息
	isRead = models.BooleanField() #
	isMailSend = models.BooleanField()  #
	isFavor = models.BooleanField() #
	isWeiXinSend = models.BooleanField() #

	class Meta:
		verbose_name='UserBidding'
		verbose_name_plural=verbose_name