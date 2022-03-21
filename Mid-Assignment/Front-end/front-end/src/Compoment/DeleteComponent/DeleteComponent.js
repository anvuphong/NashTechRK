import React from "react";
import { Button, Modal } from "react-bootstrap";

const DeleteComponent = (props) => {
    return (
        <Modal
            {...props}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            backdrop="static"
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Delete a {props.model}
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <h5>Are you sure?</h5>
                <p>
                    {props.model} will be removed from the database
                </p>
            </Modal.Body>
            <Modal.Footer>
                <Button onClick={props.onHide}>Close</Button>
            </Modal.Footer>
        </Modal>
    )
}
export default DeleteComponent;