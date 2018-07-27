#! /usr/bin/env python2.7
# -*- coding: utf-8 -*-

from django.conf.urls import url, include

from category.views import CategoryListView, CategoryCreateView
from category.views import CategoryDetailView, CategoryUpdateView
from category.views import TagListView, TagDetailView
from category.views import TagCreateView, TagUpdateView

from category.api import CategoryViewSet, TagViewSet

from rest_framework import routers
from rest_framework.documentation import include_docs_urls

urlpatterns = [
    url(
        r'^category/$',
        CategoryListView.as_view(),
        name="category-home"
        ),
    url(
        r'^category/page/(?P<page>\w+)/$',
        CategoryListView.as_view(),
        name="category-home-paginated"
    ),
    url(
        r'^category/add/$',
        CategoryCreateView.as_view(),
        name="category-add"
    ),
    url(r'^category/id/(?P<pk>\d+)/$',
        CategoryDetailView.as_view(), name="category-detail"),
    url(r'^category/name/(?P<slug>[-\w]+)/$',
        CategoryDetailView.as_view(), name="category-detail"),
    url(
        r'^category/(?P<pk>\d+)/update$',
        CategoryUpdateView.as_view(),
        name="category-update"
    ),
    url(
        r'^category/(?P<slug>\w+)/update$',
        CategoryUpdateView.as_view(),
        name="category-update"
    ),
]

urlpatterns += [
    url(
        r'^tag /$',
        TagListView.as_view(),
        name="tag-home"
    ),
    url(
        r'^tag/page/(?P<page>\w+)/$',
        TagListView.as_view(),
        name="tag-home-paginated"
    ),
    url(
        r'^tag/add/$',
        TagCreateView.as_view(),
        name="tag-add"
    ),
    url(
        r'^tag/(?P<slug>[\w-]+)/$',
        TagDetailView.as_view(),
        name="tag-detail"
    ),
    url(
        r'^tag/(?P<id>\d+)/update/$',
        TagUpdateView.as_view(),
        name="tag-update"
    ),
]


# Routers provide an easy way of automatically determining the URL conf.
router = routers.DefaultRouter(trailing_slash=False)
router.register(r'category', CategoryViewSet, base_name="api_category")
router.register(r'tag', TagViewSet, base_name="api_tag")

urlpatterns += [
    url(r'^api/', include(router.urls)),
    url(r'^docs/', include_docs_urls(title='Category API'))
]
