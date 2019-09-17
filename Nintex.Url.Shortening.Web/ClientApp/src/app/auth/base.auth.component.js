"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var BaseAuthComponent = /** @class */ (function () {
    function BaseAuthComponent(api) {
        this.api = api;
    }
    //#region Supported Methods
    BaseAuthComponent.prototype.validateInput = function () {
        if (!this.username || this.username.length < 1) {
            throw 'Please enter username';
        }
        if (!this.password || this.password.length < 1) {
            throw 'Please enter password';
        }
    };
    return BaseAuthComponent;
}());
exports.BaseAuthComponent = BaseAuthComponent;
//# sourceMappingURL=base.auth.component.js.map