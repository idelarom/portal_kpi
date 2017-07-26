﻿(function () {
    function e(a) { l.call(this, a); this.hintContainer = this.codeContainer = null } var l = ToolbarConfigurator.AbstractToolbarModifier, g = ToolbarConfigurator.FullToolbarEditor; ToolbarConfigurator.ToolbarTextModifier = e; e.prototype = Object.create(l.prototype); e.prototype._onInit = function (a, d) { l.prototype._onInit.call(this, void 0, d); this._createModifier(d ? this.actualConfig : void 0); "function" === typeof a && a(this.mainContainer) }; e.prototype._createModifier = function (a) {
        function d(a) {
            var b = c(a); if (null !== b.charsBetween) {
                var d =
                k.getUnusedButtonsArray(k.actualConfig.toolbar, !0, b.charsBetween), e = a.getCursor(), b = CodeMirror.Pos(e.line, e.ch - b.charsBetween.length), h = a.getTokenAt(e); "{" === a.getTokenAt({ line: e.line, ch: h.start }).string && (d = ["name"]); if (0 !== d.length) return new f(b, e, d)
            }
        } function f(a, c, b) { this.from = a; this.to = c; this.list = b; this._handlers = [] } function c(a, c) {
            var b = {}; b.cur = a.getCursor(); b.tok = a.getTokenAt(b.cur); b["char"] = c || b.tok.string.charAt(b.tok.string.length - 1); var d = a.getRange(CodeMirror.Pos(b.cur.line, 0), b.cur).split("").reverse().join(""),
            d = d.replace(/(['|"]\w*['|"])/g, ""); b.charsBetween = d.match(/(^\w*)(['|"])/); b.charsBetween && (b.endChar = b.charsBetween[2], b.charsBetween = b.charsBetween[1].split("").reverse().join("")); return b
        } function b(a) { setTimeout(function () { a.state.completionActive || CodeMirror.showHint(a, d, { hintsClass: "toolbar-modifier", completeSingle: !1 }) }, 100); return CodeMirror.Pass } var k = this; this._createToolbar(); this.toolbarContainer && this.mainContainer.append(this.toolbarContainer); l.prototype._createModifier.call(this);
        this._setupActualConfig(a); a = this.actualConfig.toolbar; a = CKEDITOR.tools.isArray(a) ? "\tconfig.toolbar \x3d " + ("[\n\t\t" + g.map(a, function (a) { return l.stringifyJSONintoOneLine(a, { addSpaces: !0, noQuotesOnKey: !0, singleQuotes: !0 }) }).join(",\n\t\t") + "\n\t]") + ";" : "config.toolbar \x3d [];"; a = ["CKEDITOR.editorConfig \x3d function( config ) {\n", a, "\n};"].join(""); var e = new CKEDITOR.dom.element("div"); e.addClass("codemirror-wrapper"); this.modifyContainer.append(e); this.codeContainer = CodeMirror(e.$, {
            mode: {
                name: "javascript",
                json: !0
            }, lineNumbers: !1, lineWrapping: !0, viewportMargin: Infinity, value: a, smartIndent: !1, indentWithTabs: !0, indentUnit: 4, tabSize: 4, theme: "neo", extraKeys: { Left: b, Right: b, "'''": b, "'\"'": b, Backspace: b, Delete: b, "Shift-Tab": "indentLess" }
        }); this.codeContainer.on("endCompletion", function (a, b) { var d = c(a); void 0 !== b && a.replaceSelection(d.endChar) }); this.codeContainer.on("change", function () {
            var a = k.codeContainer.getValue(), a = k._evaluateValue(a); null !== a ? (k.actualConfig.toolbar = a.toolbar ? a.toolbar : k.actualConfig.toolbar,
            k._fillHintByUnusedElements(), k._refreshEditor(), k.mainContainer.removeClass("invalid")) : k.mainContainer.addClass("invalid")
        }); this.hintContainer = new CKEDITOR.dom.element("div"); this.hintContainer.addClass("toolbarModifier-hints"); this._fillHintByUnusedElements(); this.hintContainer.insertBefore(e)
    }; e.prototype._fillHintByUnusedElements = function () {
        var a = this.getUnusedButtonsArray(this.actualConfig.toolbar, !0), a = this.groupButtonNamesByGroup(a), d = g.map(a, function (a) {
            var b = g.map(a.buttons, function (a) {
                return "\x3ccode\x3e" +
                a + "\x3c/code\x3e "
            }).join(""); return ["\x3cdt\x3e\x3ccode\x3e", a.name, "\x3c/code\x3e\x3c/dt\x3e\x3cdd\x3e", b, "\x3c/dd\x3e"].join("")
        }).join(" "), f = '\x3cdt class\x3d"list-header"\x3eToolbar group\x3c/dt\x3e\x3cdd class\x3d"list-header"\x3eUnused items\x3c/dd\x3e'; a.length || (f = "\x3cp\x3eAll items are in use.\x3c/p\x3e"); this.codeContainer.refresh(); this.hintContainer.setHtml("\x3ch3\x3eUnused toolbar items\x3c/h3\x3e\x3cdl\x3e" + f + d + "\x3c/dl\x3e")
    }; e.prototype.getToolbarGroupByButtonName = function (a) {
        var d =
        this.fullToolbarEditor.buttonNamesByGroup, f; for (f in d) for (var c = d[f], b = c.length; b--;) if (a === c[b]) return f; return null
    }; e.prototype.getUnusedButtonsArray = function (a, d, f) { d = !0 === d ? !0 : !1; var c = e.mapToolbarCfgToElementsList(a); a = Object.keys(this.fullToolbarEditor.editorInstance.ui.items); a = g.filter(a, function (a) { var d = "-" === a; a = void 0 === f || 0 === a.toLowerCase().indexOf(f.toLowerCase()); return !d && a }); a = g.filter(a, function (a) { return -1 == CKEDITOR.tools.indexOf(c, a) }); d && a.sort(); return a }; e.prototype.groupButtonNamesByGroup =
    function (a) { var d = [], f = JSON.parse(JSON.stringify(this.fullToolbarEditor.buttonNamesByGroup)), c; for (c in f) { var b = f[c], b = g.filter(b, function (b) { return -1 !== CKEDITOR.tools.indexOf(a, b) }); b.length && d.push({ name: c, buttons: b }) } return d }; e.mapToolbarCfgToElementsList = function (a) { function d(a) { return "-" !== a } for (var f = [], c = a.length, b = 0; b < c; b += 1) a[b] && "string" !== typeof a[b] && (f = f.concat(g.filter(a[b].items, d))); return f }; e.prototype._setupActualConfig = function (a) {
        a = a || this.editorInstance.config; CKEDITOR.tools.isArray(a.toolbar) ||
        (a.toolbarGroups || (a.toolbarGroups = this.fullToolbarEditor.getFullToolbarGroupsConfig(!0)), this._fixGroups(a), a.toolbar = this._mapToolbarGroupsToToolbar(a.toolbarGroups, this.actualConfig.removeButtons), this.actualConfig.toolbar = a.toolbar, this.actualConfig.removeButtons = "")
    }; e.prototype._mapToolbarGroupsToToolbar = function (a, d) {
        d = d || this.editorInstance.config.removedBtns; d = "string" == typeof d ? d.split(",") : []; for (var f = a.length; f--;) {
            var c = this._mapToolbarSubgroup(a[f], d); "separator" === a[f].type ? a[f] = "/" :
            CKEDITOR.tools.isArray(c) && 0 === c.length ? a.splice(f, 1) : a[f] = "string" == typeof c ? c : { name: a[f].name, items: c }
        } return a
    }; e.prototype._mapToolbarSubgroup = function (a, d) { if ("string" == typeof a) return a; for (var f = a.groups ? a.groups.length : 0, c = [], b = 0; b < f; b += 1) { var e = a.groups[b], e = this.fullToolbarEditor.buttonsByGroup["string" === typeof e ? e : e.name] || [], e = this._mapButtonsToButtonsNames(e, d), g = e.length, c = c.concat(e); g && c.push("-") } "-" == c[c.length - 1] && c.pop(); return c }; e.prototype._mapButtonsToButtonsNames = function (a,
    d) { for (var f = a.length; f--;) { var c = a[f], c = "string" === typeof c ? c : this.fullToolbarEditor.getCamelCasedButtonName(c.name); -1 !== CKEDITOR.tools.indexOf(d, c) ? a.splice(f, 1) : a[f] = c } return a }; e.prototype._evaluateValue = function (a) { var d; try { var f = {}; Function("var CKEDITOR \x3d {}; " + a + "; return CKEDITOR;")().editorConfig(f); d = f; for (var c = d.toolbar.length; c--;) d.toolbar[c] || d.toolbar.splice(c, 1) } catch (b) { d = null } return d }; e.prototype.mapToolbarToToolbarGroups = function (a) {
        function d(a, b) {
            a = a.slice(); for (var d =
            b.length; d--;) { var c = a.indexOf(b[d]); -1 !== c && a.splice(c, 1) } return a
        } for (var f = {}, c = [], b = [], c = a.length, e = 0; e < c; e++) if ("/" === a[e]) b.push("/"); else { var g = a[e].items, m = {}; m.name = a[e].name; m.groups = []; for (var l = g.length, p = 0; p < l; p++) { var n = g[p]; if ("-" !== n) { var h = this.getToolbarGroupByButtonName(n); -1 === m.groups.indexOf(h) && m.groups.push(h); f[h] = f[h] || {}; h = f[h].buttons = f[h].buttons || {}; h[n] = h[n] || { used: 0, origin: m.name }; h[n].used++ } } b.push(m) } c = function (a, b) {
            var c = [], e; for (e in a) var f = a[e], g = b[e].slice(),
            c = c.concat(d(g, Object.keys(f.buttons))); return c
        }(f, this.fullToolbarEditor.buttonNamesByGroup); return { toolbarGroups: b, removeButtons: c.join(",") }
    }; return e
})();