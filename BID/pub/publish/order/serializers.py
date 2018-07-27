from datetime import date

from django.contrib.auth import get_user_model
from django.utils.translation import ugettext_lazy as _

from rest_framework import serializers
from rest_framework.reverse import reverse
from .scribe_model.subscribe import *

class UsersubscribeSerializer(serializers.ModelSerializer):

    class Meta:
        model = UserSubscribe
        fields = ('id', 'User', 'ScribeName', 'OrgCategory','OrgName', 'PurchseArea', 'AreaName', 'PurchaseCategory','PurchaseName', 'SeqNo', 'KeyWord', )

    def validate_end(self, value):
        new = self.instance is None
        changed = self.instance and self.instance.end != value
        if (new or changed) and (value < date.today()):
            msg = _('End date cannot be in the past.')
            raise serializers.ValidationError(msg)
        return value
