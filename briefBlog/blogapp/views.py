from django.shortcuts import render,redirect
from django.contrib.auth import authenticate
# from django.contrib.auth.models import User
from django.template import Context,Template
from django.http import HttpResponse
from blogapp.forms import Registerfm

# Create your views here.
def index(request):
	# print("i am in index...")
	context={}
	username=request.session.get('username','')
	context['username']=username
	return render(request,'index.html',context)

def about(request):
	# print("i am in about...")
	context={}
	username=request.session.get('username','')
	context['username']=username
	return render(request,'about.html',context)

def post(request):
	context={}
	username=request.session.get('username','')
	context['username']=username
	return render(request,'post.html',context)

def contact(request):
	context={}
	# username=request.COOKIES.get("username",'')
	# if username!='':
	# 	context['username']=username
	# 	return render(request,'contact.html',context)
	# else:
	# 	return render(request,'tologin.html',context)

	#get session
	username=request.session.get('username','')
	print('----------',username)
	if username:
		context['username']=username
		return render(request,'contact.html',context)
	else:
		return render(request,'tologin.html',context)
		

def register(request):
	context={}
	if request.method=='GET':
		registerfm=Registerfm()
		context['registerfm']=registerfm
		return render(request,'register.html',context)
	else:
		username=request.POST.get('username')
		password_set=request.POST.get('password_set')
		password_confirm=request.POST.get('password_confirm')
		email=request.POST.get('email')
		if password_set==password_confirm:
			Account.objects.create(
			# Account.objects.create_user(
				username=username,
				password=password_set,
				email=email,
				)
			return HttpResponse("Register Success")
		else:
			return HttpResponse("Your password is not match")


def login(request):
	context={}
	if request.method=='GET':
		username=request.session.get('username','')
		context['username']=username
		return render(request,'login.html',context)
	else:
		username=request.POST.get('username')
		password=request.POST.get('password')
		user = Account.objects.filter(username__exact=username,password__exact=password)
		# user = authenticate(username=username, password=password) 
		# if user is not None and user.is_active:
		# if user.exists():
		# 	response=redirect(to='contact')
		# 	response.set_cookie("username",username,max_age=3600)
		# 	return response

		#session 的设置
		if user.exists():
			request.session['username']=username
			return redirect(to='contact')
		else:
			return HttpResponse("Login failed,please go back to try it again")

def logout(request):
		context={}

		#cookie的设置
		# response=redirect(to='login')
		# # response=HttpResponse('logout')
		# response.delete_cookie('username')
		# # 注意返回response,不能是其他，否则cookie没有被删
		# return response
		#session的设置
		try:
			del request.session['username']
		except:
			pass
		return redirect(to='login')