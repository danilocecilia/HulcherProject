var map;
var iconSize = new OpenLayers.Size(20, 34);
var iconOffset = new OpenLayers.Pixel(-(iconSize.w / 2), -iconSize.h);
var iconJ = new OpenLayers.Icon("/Images/mj.png", iconSize, iconOffset);
var iconR = new OpenLayers.Icon("/Images/mr.png", iconSize, iconOffset);
var popupClass = OpenLayers.Class(OpenLayers.Popup.FramedCloud, {
    "autoSize": true,
    "keepInMap": true
});
var zoom, center, currentPopup, map, markers;
var bounds = new OpenLayers.Bounds();
var mapServerPath;

OpenLayers.Control.PanZoom.prototype.draw = function (px) {
    // initialize our internal div
    OpenLayers.Control.prototype.draw.apply(this, arguments);
    px = this.position;

    // place the controls
    this.buttons = [];

    var sz = new OpenLayers.Size(18, 18);
    var centered = new OpenLayers.Pixel(px.x + sz.w / 2, px.y);

    this._addButton("panup", "north-mini.png", centered, sz);
    px.y = centered.y + sz.h;
    this._addButton("panleft", "west-mini.png", px, sz);
    this._addButton("panright", "east-mini.png", px.add(sz.w, 0), sz);
    this._addButton("pandown", "south-mini.png",
                        centered.add(0, sz.h * 2), sz);
    this._addButton("zoomin", "zoom-plus-mini.png",
                        centered.add(0, sz.h * 3.5 + 5), sz);
    this._addButton("zoomout", "zoom-minus-mini.png",
                        centered.add(0, sz.h * 4.5 + 5), sz);
    return this.div;
};


function GetSourceDataMapPlot(lstDataMapPlot, mapServerUrl) {
    if (lstDataMapPlot != null && lstDataMapPlot.length > 0) {
        
        if(mapServerUrl != null)
            mapServerPath = mapServerUrl;

        markers.clearMarkers();

        for (var i = 0; i < lstDataMapPlot.length; i++) {

            addMarker(lstDataMapPlot[i].Longitude, lstDataMapPlot[i].Latitude, lstDataMapPlot[i].Type, lstDataMapPlot[i].Name + "<br />" + lstDataMapPlot[i].Description);
        }
    }
}

function addMarker(lng, lat, type, info) {
    var pt = new OpenLayers.LonLat(lng, lat);
//                            .transform(new OpenLayers.Projection("EPSG:4326"),
//                            map.getProjectionObject());
    bounds.extend(pt);
    var feature = new OpenLayers.Feature(markers, pt);
    feature.closeBox = true;
    feature.popupClass = popupClass;
    feature.data.popupContentHTML = info;
    feature.data.overflow = "auto";

    var markerIcon;

    if (type == "J")
        markerIcon = iconJ.clone();
    else
        markerIcon = iconR.clone();

    var marker = new OpenLayers.Marker(pt, markerIcon);

    var markerOver = function(evt) {
        if (currentPopup != null && currentPopup.visible()) {
            currentPopup.hide();
        }
        if (this.popup == null) {
            this.popup = this.createPopup(this.closeBox);
            map.addPopup(this.popup);
            this.popup.show();
        } else {
            this.popup.toggle();
        }
        currentPopup = this.popup;
        OpenLayers.Event.stop(evt);
    };
    var markerOut = function (evt) {
        if (currentPopup != null && currentPopup.visible()) {
            currentPopup.hide();
        }
        OpenLayers.Event.stop(evt);
    }
    marker.events.register("mouseover", feature, markerOver);
    //marker.events.register('mouseout', feature, markerOut);

    markers.addMarker(marker);
}

function olmapinit() {

    map = new OpenLayers.Map('map');
    //layer = new OpenLayers.Layer.MapServer("MapPlotting", "http://maps01:8080/cgi-bin/mapserv.exe", { map: "C:/ms4w/Apache/htdocs/mapfile.map" }, { maxExtent: new OpenLayers.Bounds(-180, -90, 180, 90) });
    layer = new OpenLayers.Layer.MapServer("MapPlotting", mapServerPath, { map: "C:/ms4w/Apache/htdocs/mf.map" }, { maxExtent: new OpenLayers.Bounds(-180, -90, 180, 90) });
    map.addLayer(layer);

    map.zoomToMaxExtent();

    markers = new OpenLayers.Layer.Markers("Markers");
    map.addLayer(markers);
}

OpenLayers.Event.observe(window, "load", olmapinit);

