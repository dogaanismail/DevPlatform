import { FormGroup } from "@angular/forms";
import { ConfirmedValidator } from "../validators/passsword-match.validators";

const DATA_STEP_1 = {
    firstName: { type: 'text', validations: { required: true }, class: "input", errors: { required: 'This field can not be left blank' }, placeholder: 'Enter your first name' },
    lastName: { type: 'text', validations: { required: true }, errors: { required: 'This field can not be left blank' }, class: "input", placeholder: 'Enter your last name' },
    userName: { type: 'text', validations: { required: true }, errors: { required: 'This field can not be left blank' }, class: "input", placeholder: 'Enter your user name' },
    email: {
        type: 'text',
        validations: {
            required: true,
            pattern: /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        },
        errors: {
            pattern: 'Please enter a valid email address',
            required: 'This field can not be left blank'
        }, class: "input", placeholder: 'Enter your email address'
    }
};


const DATA_STEP_2 = {
    password: {
        type: 'password', validations: { required: true },
        class: "input", errors: { required: 'This field can not be left blank' }, placeholder: 'Enter your password', validator: ConfirmedValidator('password', 'rePassword')
    },
    rePassword: { type: 'password', validations: { required: true }, class: "input", errors: { required: 'This field can not be left blank' }, placeholder: 'Enter your re-password' },
};

const STEP_ITEMS = [
    { label: 'Tell us more about you.', data: DATA_STEP_1 },
    { label: 'Secure your account.', data: DATA_STEP_2 },
    { label: 'Review & Submit', data: {} }
];

export { STEP_ITEMS }
