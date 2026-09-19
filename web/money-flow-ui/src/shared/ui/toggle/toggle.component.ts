import { Component, Input, forwardRef, signal, Output, EventEmitter, booleanAttribute } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
    selector: 'app-toggle',
    standalone: true,
    providers: [{
        provide: NG_VALUE_ACCESSOR,
        useExisting: forwardRef(() => ToggleComponent),
        multi: true
    }],
    templateUrl: 'toggle.component.html',
    styleUrl: 'toggle.component.scss'
})
export class ToggleComponent implements ControlValueAccessor {
    @Input({ transform: booleanAttribute }) 
    set value(val: boolean) {
        this.checked.set(val);
    }
    get value(): boolean {
        return this.checked();
    }
    
    @Output() valueChange = new EventEmitter<boolean>();
    
    @Input({ transform: booleanAttribute }) disabled: boolean = false;

    checked = signal<boolean>(false);

    private onChange: (value: boolean) => void = () => {};
    private onTouch: () => void = () => {};

    toggle() {
        if (this.disabled) return;
        
        const newValue = !this.checked();
        this.checked.set(newValue);
        this.onChange(newValue);
        this.onTouch();
        this.valueChange.emit(newValue);
    }

    writeValue(value: boolean): void {
        this.checked.set(!!value);
    }

    registerOnChange(fn: any): void {
        this.onChange = fn;
    }

    registerOnTouched(fn: any): void {
        this.onTouch = fn;
    }

    setDisabledState(isDisabled: boolean): void {
        this.disabled = isDisabled;
    }
}