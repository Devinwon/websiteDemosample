#! /usr/bin/env python2.7
# -*- coding: utf-8 -*-

from .models import Tag
from .models import Category

from rest_framework import viewsets
from rest_framework import serializers


class CategorySerializer(serializers.ModelSerializer):
    class Meta:
        model = Category
        fields = '__all__'


class TagSerializer(serializers.ModelSerializer):
    class Meta:
        model = Tag
        fields = '__all__'


class CategoryViewSet(viewsets.ModelViewSet):
    """
    retrieve:
    Return the given category.

    list:
    Return a list of all the existing categories.

    create:
    Create a new category instance.
    """
    queryset = Category.objects.all()
    serializer_class = CategorySerializer


class TagViewSet(viewsets.ModelViewSet):
    queryset = Tag.objects.all()
    serializer_class = TagSerializer
