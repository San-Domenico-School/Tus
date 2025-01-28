import bpy, bmesh
from bpy import context
from  mathutils import Vector


# bounding box helper methods
def bbox(ob):
    return (Vector(b) for b in ob.bound_box)

def bbox_center(ob):
    return sum(bbox(ob), Vector()) / 8

def bbox_axes(ob):
    bb = list(bbox(ob))
    return tuple(bb[i] for i in (0, 4, 3, 1))

def slice(bm, start, end, segments):
    if segments == 1:
        return
    def geom(bm):
        return bm.verts[:] + bm.edges[:] + bm.faces[:]
    planes = [start.lerp(end, f / segments) for f in range(1, segments)]
    #p0 = start
    plane_no = (end - start).normalized() 
    while(planes): 
        p0 = planes.pop(0)                 
        ret = bmesh.ops.bisect_plane(bm, 
                geom=geom(bm),
                plane_co=p0, 
                plane_no=plane_no)
        bmesh.ops.split_edges(bm, 
                edges=[e for e in ret['geom_cut'] 
                if isinstance(e, bmesh.types.BMEdge)])
        
def calculateNumberOfSegments(start, end):
    splitLength = 20

    diff = end - start
    length = diff.length
    print(length)
    return int(length / splitLength)


# Get the selected objects
selected_objects = bpy.context.selected_objects



# Aplly rotation and scale and cut up
for obj in selected_objects :
    bpy.ops.object.transform_apply(location=False, rotation=True, scale=True)
     
    bm = bmesh.new()
    ob = context.object
    me = ob.data
    bm.from_mesh(me)

    o, x, y, z = bbox_axes(ob)        

    x_segments = calculateNumberOfSegments(o, x)
    y_segments = calculateNumberOfSegments(o, y)
    z_segments = calculateNumberOfSegments(o, z)
    
    if x_segments == 1 or y_segments == 1 or z_segments == 1 :
        continue

    slice(bm, o, x, x_segments)
    slice(bm, o, y, y_segments)
    slice(bm, o, z, z_segments)    
    bm.to_mesh(me)

    bpy.ops.object.mode_set(mode='EDIT')
    bpy.ops.mesh.separate(type='LOOSE')
    bpy.ops.object.mode_set() 



# Get the selected objects
selected_objects = bpy.context.selected_objects


objs = [o for o in selected_objects]
bpy.ops.object.select_all(action='DESELECT')


# Loop over the selected objects with only one selected at a time
for obj in objs:

    # Select the object 
    bpy.context.view_layer.objects.active = obj
    
        
    # Enter edit mode
    bpy.ops.object.mode_set(mode='EDIT')

    # Select all faces
    bpy.ops.mesh.select_all(action='SELECT')

    bpy.ops.uv.smart_project(angle_limit=1.15192, margin_method='ADD', rotate_method='AXIS_ALIGNED_Y', island_margin=.03)


    # Exit edit mode
    bpy.ops.object.mode_set(mode='OBJECT')



[o.select_set(True) for o in selected_objects]