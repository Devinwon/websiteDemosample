from rest_framework.routers import DefaultRouter
from  . import views
from . import bid_views
from django.conf.urls import include, url

router = DefaultRouter()
router.register(r'bidding_infos', views.BiddingInfoViewSet )
router.register(r'bid_infos', bid_views.BidInfoViewSet )
router.register(r'user_scribes', views.UserSubscribeViewSet )
router.register(r'area_infos', views.AreaViewSet )
router.register(r'purcate_infos', views.PurchaseCategoryViewSet )
