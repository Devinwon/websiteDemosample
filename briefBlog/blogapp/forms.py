
from django import forms
from django.contrib.auth.models import User
'''
register form
'''
class Registerfm(forms.ModelForm):
	class Meta():
		model=User
		fields=('username','password','email')
		# exclude=('id',)




# from blogapp.models import User
# class Loginfm(forms.ModelForm):
# 	def clean(self):
# 		# 调用父类的方法
# 		# 获取表单所有的数据
# 		cleaned_data=super(Loginfm,self).clean()
# 		value=cleaned_data.get("username")
# 		try:
# 			User.objects.get(username=value)
# 			self._errors['username']=self.error_class(["%s已经存在"%value])
			
# 		except User.DoesNotExist:
# 			pass
# 		return cleaned_data


# 	# 与ModelForm中字段重名不会添加，这里用于给字段加验证器
# 	# username=forms.CharField(validators=[validate_name])
# 	# 绑定User类
# 	class Meta():
# 		model=User
# 		# 除了'ID'外全部显示，include('ID')只包含'ID',相反
# 		exclude=("id",'email')