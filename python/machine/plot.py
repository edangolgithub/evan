import matplotlib
matplotlib.use('Agg')
import numpy as np
from numpy import pi
import matplotlib.pyplot as plt
plt.switch_backend('agg')

x = np.arange(-pi, pi, pi/1000)
y1 = np.sin(x)
y2 = np.cos(x)
plt.title("Circular Functions")
plt.xlabel("x")
plt.ylabel("y")
plt.plot(x, y1)
plt.plot(x, y2)
plt.grid(True)
plt.savefig("mygraph.png")
plt.show()