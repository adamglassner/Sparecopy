"""Generates the Sparecopy icon. Run: python tools/make_icon.py
Writes src/Sparecopy.Desktop/Assets/sparecopy.ico + .png, assets/sparecopy-256.png (used by the
Bytehawk Studio Launcher) and tools/icon-preview.png."""
from PIL import Image, ImageDraw, ImageFilter
import os

S = 1024
here = os.path.dirname(os.path.abspath(__file__))
root = os.path.join(here, "..")


def lerp(a, b, t):
    return tuple(int(a[i] + (b[i] - a[i]) * t) for i in range(len(a)))


img = Image.new("RGBA", (S, S), (0, 0, 0, 0))

# Rounded-square background: charcoal with a soft teal glow at the top
grad = Image.new("RGBA", (S, S))
gd = ImageDraw.Draw(grad)
for y in range(S):
    gd.line([(0, y), (S, y)], fill=lerp((22, 70, 62, 255), (17, 19, 22, 255), min(1, y / (S * 0.85))))
mask = Image.new("L", (S, S), 0)
ImageDraw.Draw(mask).rounded_rectangle([24, 24, S - 24, S - 24], radius=220, fill=255)
img.paste(grad, (0, 0), mask)


# Back card (the spare copy): mint, up and to the right
back = [400, 210, 800, 690]
shadow = Image.new("RGBA", (S, S), (0, 0, 0, 0))
ImageDraw.Draw(shadow).rounded_rectangle([b + 14 for b in back], radius=80, fill=(0, 0, 0, 120))
img.alpha_composite(shadow.filter(ImageFilter.GaussianBlur(22)))
d = ImageDraw.Draw(img)
d.rounded_rectangle(back, radius=80, fill=(127, 211, 193, 255))

# Front card (the original): white, down and to the left, with a soft shadow
front = [224, 334, 624, 814]
shadow = Image.new("RGBA", (S, S), (0, 0, 0, 0))
ImageDraw.Draw(shadow).rounded_rectangle([front[0] + 10, front[1] + 22, front[2] + 10, front[3] + 22], radius=80, fill=(0, 0, 0, 150))
img.alpha_composite(shadow.filter(ImageFilter.GaussianBlur(26)))
d = ImageDraw.Draw(img)
d.rounded_rectangle(front, radius=80, fill=(255, 255, 255, 255))

# A teal circular "copy / restore" arrow on the front card
import math
teal = (14, 107, 92, 255)
cx, cy, r, w = 424, 574, 104, 44
a0, a1 = -50, 205  # PIL angles: degrees clockwise from 3 o'clock
d.arc([cx - r, cy - r, cx + r, cy + r], start=a0, end=a1, fill=teal, width=w)
# Round cap at the tail
t = math.radians(a0)
tx, ty = cx + (r - w / 2) * math.cos(t), cy + (r - w / 2) * math.sin(t)
d.ellipse([tx - w / 2, ty - w / 2, tx + w / 2, ty + w / 2], fill=teal)
# Arrowhead at the end, pointing along the clockwise tangent
e = math.radians(a1)
ex, ey = cx + (r - w / 2) * math.cos(e), cy + (r - w / 2) * math.sin(e)
dx, dy = -math.sin(e), math.cos(e)      # clockwise tangent in screen space
nx, ny = math.cos(e), math.sin(e)       # outward normal
tip = (ex + dx * 70, ey + dy * 70)
b1 = (ex + nx * 58 - dx * 6, ey + ny * 58 - dy * 6)
b2 = (ex - nx * 58 - dx * 6, ey - ny * 58 - dy * 6)
d.polygon([tip, b1, b2], fill=teal)

out = os.path.join(root, "src", "Sparecopy.Desktop", "Assets")
os.makedirs(out, exist_ok=True)
img.resize((256, 256), Image.LANCZOS).save(os.path.join(out, "sparecopy.png"))
img.save(os.path.join(out, "sparecopy.ico"),
         sizes=[(16, 16), (20, 20), (24, 24), (32, 32), (40, 40), (48, 48), (64, 64), (128, 128), (256, 256)])
os.makedirs(os.path.join(root, "assets"), exist_ok=True)
img.resize((256, 256), Image.LANCZOS).save(os.path.join(root, "assets", "sparecopy-256.png"))
img.resize((512, 512), Image.LANCZOS).save(os.path.join(here, "icon-preview.png"))
print("ok")
