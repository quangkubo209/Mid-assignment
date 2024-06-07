const InputField = (props) => {
    const {name, id, type="text", label, value, onChange, disabled=false} = props;
    return (
        <div className="bg-slate-200 p-2 w-[300px]">
            <label htmlFor={id} className="block text-sm">{label}</label>
            <input 
                disabled={disabled}
                value={value}
                type={type} 
                name={name}
                id={id}
                onChange={onChange}
                className="focus:outline-none bg-transparent w-full focus:bg-black focus:bg-opacity-10 focus:p-2 transition-all"
            />
        </div>
    );
};

export default InputField;