import { useEffect, useState } from 'react'
import { Button, Form } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';

type Props = {
    values: string[];
    onChange: (event: any) => void;
    name?: string;
    id?: string;
}

function DynamicTextListInput(props: Props) {
    const [values, setValues] = useState<string[]>(props.values ?? [''])
    const { t } = useTranslation();
    useEffect(() => {
        props.onChange({ target: { value: values, id: props.id, name: props.name } });
    }, [values])
    return (
        <div id={props.id}>
            {
                values.map((v: string, i: number) =>
                    <div key={i} className="d-flex gap-2 mb-2 ">
                        <Form.Control
                            type='text'
                            value={v}
                            id={`dynamic-${i}`}
                            name="dynamic"
                            onChange={(evt) => {
                                const newValues = values.slice();
                                newValues[i] = evt.target.value;
                                setValues(newValues);
                            }} />
                        <Button onClick={() => { setValues([...values.filter((_, k) => i != k)]) }}>{t('removeField')}</Button>
                    </div>
                )
            }
            <div className=' d-flex justify-content-end '>
                <Button onClick={() => { setValues([...values, ""]) }}>{t('addField')}</Button>
            </div>
        </div>
    )
}

export default DynamicTextListInput