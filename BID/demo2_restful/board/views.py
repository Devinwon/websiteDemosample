from django.contrib.auth import get_user_model

from rest_framework import authentication, permissions, viewsets, filters

from .models import Sprint, Task
from .serializers import SprintSerializer,TaskSerializer,UserSerializer
# Create your views here.
User = get_user_model()


class DefaultsMixin(object):
    """Default settings for view authentication, permissions, filtering
     and pagination."""
    
    authentication_classes = (
        authentication.BasicAuthentication,
        authentication.TokenAuthentication,    
    )
    permission_classes = (
        permissions.IsAuthenticated,
    )
    paginate_by = 25
    paginate_by_param = 'page_size'
    max_paginate_by = 100


class SprintViewSet(DefaultsMixin, viewsets.ModelViewSet):
    """API endpoint for listing and creating sprints."""
    
    queryset = Sprint.objects.order_by('end')
    serializer_class = SprintSerializer
    

class TaskViewSet(DefaultsMixin, viewsets.ModelViewSet):
    """API endpoint for listing and creating tasks."""
    
    queryset = Task.objects.all()
    serializer_class = TaskSerializer
    search_fields = ('name', 'description', )
    ordering_fields = ('name', 'order', 'started', 'due', 'completed', )
    
    
class UserViewSet(DefaultsMixin, viewsets.ReadOnlyModelViewSet):
    """API endpoint for listing users."""
    
    lookup_field = User.USERNAME_FIELD
    lookup_url_kwarg = User.USERNAME_FIELD
    queryset = User.objects.order_by(User.USERNAME_FIELD)
    serializer_class = UserSerializer
    search_fields = (User.USERNAME_FIELD, )