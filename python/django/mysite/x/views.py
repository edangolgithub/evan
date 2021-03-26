from django.shortcuts import render
from x.form import StuForm  
# Create your views here.
from django.http import HttpResponse  
  
def hello(request):  
    return HttpResponse("<h2>Hello, Welcome to Django!</h2>")  
def frm(request):  
    stu = StuForm()  
    return render(request,"form.html",{'form':stu})   