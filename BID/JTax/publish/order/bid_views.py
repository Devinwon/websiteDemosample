from django.shortcuts import get_object_or_404
from django.core.paginator import Paginator, EmptyPage, PageNotAnInteger

from rest_framework.decorators import api_view
from rest_framework.response import Response
from rest_framework import status
from rest_framework import viewsets
from django.db.models import Q
from .models import BiddingInfo
from .api import BiddingInfoSerializer


class BidInfoViewSet( viewsets.ModelViewSet):
    queryset = BiddingInfo.objects.order_by('Title')
    serializer_class  = BiddingInfoSerializer

    def list(self, request):
        #bidInfos = BiddingInfo.objects.all().order_by('-id')
        orgcate = request.GET.get('orgcate')
        area = request.GET.get('area')
        purcate = request.GET.get('purcate')
        status = request.GET.get('status')
        title = request.GET.get('title')
        orderby = request.GET.get('orderby')
        searchCondition = {}
        w = Q()
        if orgcate:
            searchCondition['OrgCategory'] = orgcate
            w=w&Q(OrgCategory__icontains =title)
        if area:
            searchCondition['PurchseArea'] = area
            w=w&Q(PurchseArea__exact=area)
        if purcate:
            searchCondition['PurchaseCategory'] = purcate
            w = w&Q(PurchaseCategory__exact=purcate)
        if status:
            w = w&Q(BiddingStatus__exact=status)
        if title:
            searchCondition['Title'] = title
            w= w& Q(Title__icontains = title)

        c_page_size = request.GET.get('page_size')
        if c_page_size:
            page_size = int (c_page_size)
            if page_size>100:
                page_size =100
            if page_size<1:
                page_size=5

        bidInfos=BiddingInfo.objects.filter(w).order_by('-id')
        paginator = Paginator(bidInfos,int(page_size) )

        page = request.query_params.get('page')
        try:
            product_item_category = paginator.page(page)
        except PageNotAnInteger:
            product_item_category = paginator.page(1)
        except EmptyPage:
            product_item_category = paginator.page(paginator.num_pages)
        serializer = BiddingInfoSerializer(product_item_category, many=True)
        return Response({'num_pages':paginator.num_pages ,'page':page, 'results':serializer.data})


    def retrieve(self, request, pk=None):
        queryset = BiddingInfo.objects.all()
        user = get_object_or_404(queryset, pk=pk)
        serializer = BiddingInfoSerializer(user)
        return Response(serializer.data)

    def create(self, request):
        serializer = BiddingInfoSerializer(data=request.data)
        # data.encode("base64")
        if serializer.is_valid():
            serializer.save()
            res_msg = {'Success_Message': 'Successful', 'Success_Code': 200}
            return Response(res_msg)
        else:
            return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)

    def update(self, request, pk=None):
        try:
            product = BiddingInfo.objects.get(pk=pk)
        except BiddingInfo.DoesNotExist:
            return Response(status=status.HTTP_404_NOT_FOUND)
        serializer = BiddingInfoSerializer(product, data=request.data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data)


    def destroy(self, request, pk=None):
        try:
            product = BiddingInfo.objects.get(pk=pk)
        except BiddingInfo.DoesNotExist:
            return Response(status=status.HTTP_404_NOT_FOUND)
        product.delete()
        return Response(status=status.HTTP_204_NO_CONTENT)


def getKwargs(data={}):
    kwargs = {}

    for (k, v) in data.items():

        if v is not None and v != u'':
            kwargs[k] = v

    return kwargs