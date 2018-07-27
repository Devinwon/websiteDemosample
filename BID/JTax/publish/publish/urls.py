"""publish URL Configuration

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/1.11/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  url(r'^$', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  url(r'^$', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.conf.urls import url, include
    2. Add a URL to urlpatterns:  url(r'^blog/', include('blog.urls'))
"""
from django.conf.urls import include, url
from django.contrib import admin
from order.api import *
from order.views import *
from order.urls import router

urlpatterns = [
    url(r'^admin/', admin.site.urls),
    url(r'^api/org/', org),
    url(r'^api/area/', area),
    url(r'^api/pvnshow/$', pvnshow),
    url(r'^api/pvnshow/(?P<prtid>\d+)/$', pvnshow),
    url(r'^api/pvnshow/(?P<prtid>\d+)/(?P<lstid>\d+)/$', pvnshow),
    url(r'^api/purcateall/', purcateall),
    url(r'^api/purcate/$', purchasecategory),
    url(r'^api/purcate/(?P<prtid>\d+)/$', purchasecategory),
    url(r'^api/purcate/(?P<prtid>\d+)/(?P<lstid>\d+)$', purchasecategory),
    url(r'^api/orderdetail/', orderdetail),
    url(r'^api/bonuspoints/', bonuspoints),
    url(r'^api/orguser/', orguser),
    url(r'^api/bidinfo/', biddinginfo),
    url(r'^api/userinfo/', userprofile),
    url(r'^api/footprint/', footprint),
    url(r'^api/orgcate/$', orgcategory),
    url(r'^api/orgcate/(?P<prtid>\d+)/$', orgcategory),
    url(r'^api/orgcateall/', orgcategory_all),
    url(r'^sms/', send_code),
    url(r'^api/', include(router.urls)),
    url(r'^api-auth/', include('rest_framework.urls', namespace='rest_framework')),
    url(r'^api/user/verify_user', verify_user ),
    url(r'^api/user/send_code', send_code ),
    url(r'^api/user/register_check', register_check ),
    url(r'^api/user/login_check', login_check ),
    url(r'^api/user/update_pwd', update_pwd ),
    url(r'^api/user/logout', logout ),
    # url(r'^$', index),

]



