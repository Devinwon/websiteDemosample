import django_filters

from .models import BiddingInfo


class NullFilter(django_filters.BooleanFilter):
	"""Filter on a field set as null or not."""

	def filter(self, qs, value):
		if value is not None:
			return qs.filter(**{'%s__isnull' % self.name: value})
		return qs


class BiddingInfoFilter(django_filters.FilterSet):
	title = django_filters.Filter(name="title", lookup_type='Title')
	area = django_filters.Filter(name="area", lookup_type='PurchseArea_id')
	class Meta:
		model = BiddingInfo
		fields = ('Title','area')

	def __init__(self, *args, **kwargs):
		super().__init__(*args, **kwargs)
		self.filters['title'].extra.update()