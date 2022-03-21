import axios from "axios";
import React, { useState } from "react";
import { Button, Form, Modal } from "react-bootstrap";

const AddCompoment = (props) => {
    const [data, setData] = useState({
        name: '',
        author: '',
        category: '0'
    });

    const handleInputChange = (evt) => {
        setData({
            ...data,
            [evt.target.name]: evt.target.value
        })
    }

    const handleSubmit = (evt) => {
        evt.preventDefault();
        props.onHide();
        if (props.model === 'Book') {
            (async () => {
                let isCancel = false;
                await axios({
                    method: 'POST',
                    url: 'https://localhost:7279/api/book',
                    data: {
                        bookName: data.name,
                        author: data.author,
                        categoryId: data.category
                    }
                })
                return () => {
                    isCancel = true;
                }
            })();
        }
        if (props.model === 'Category') {
            (async () => {
                let isCancel = false;
                await axios({
                    method: 'POST',
                    url: 'https://localhost:7279/api/category',
                    data: {
                        categoryName: data.name,
                    }
                })
                return () => {
                    isCancel = true;
                }
            })();
        }
    }

    return (
        <Modal
            {...props}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Add New {props.model}
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <h4>{props.model} Modal</h4>
                <Form onSubmit={handleSubmit}>
                    <Form.Group className="add" id="inputName">
                        <Form.Label>{props.model}</Form.Label>
                        <Form.Control name="name" type="text" onChange={handleInputChange} value={data.name}/>
                        <Form.Text muted>
                            Enter {props.model}.
                        </Form.Text>
                    </Form.Group>

                    {(props.model === 'Book') &&
                        <Form.Group className="add" id="inputAuthor">
                            <Form.Label>Author</Form.Label>
                            <Form.Control name="author" type="text" onChange={handleInputChange} value={data.author}/>
                            <Form.Text muted>
                                Enter Author.
                            </Form.Text>
                        </Form.Group>
                    }
                    {(props.model === 'Book') &&
                        <Form.Select value={Number(data.category)} name="category" onChange={handleInputChange} className="add" id="inputCategory">
                            <option value={0}>Choose a Category</option>
                            {props.categories.map(category => (
                                <option key={category.categoryId} value={category.categoryId}>{category.categoryName}</option>
                            ))}
                        </Form.Select>
                    }
                    {(props.model === 'Category' && data.name !== '') && <Button style={{marginTop:'20px'}} type="submit">Confirm</Button>}
                    {(props.model === 'Book' && data.name !== '' && data.author !== '' && data.category!=='0') &&
                    <Button style={{marginTop:'20px'}} type="submit">Confirm</Button>
                    }
                </Form>
            </Modal.Body>
        </Modal>
    )
}
export default AddCompoment;